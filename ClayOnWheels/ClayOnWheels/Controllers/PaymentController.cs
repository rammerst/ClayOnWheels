using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using ClayOnWheels.Functions;
using ClayOnWheels.Models.Payment;
using ClayOnWheels.Models.EF;

namespace ClayOnWheels.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentClient _paymentClient;
        private readonly MyDbContext _db = new MyDbContext();

        public PaymentController()
        {
            this._paymentClient = new PaymentClient(AppSettings.MollieApiKey);
        }

        // GET: Payment
        public ActionResult Wait()
        {
            return View();
        }
        public async Task<ActionResult> Feedback(string guid = "")
        {
            var payRes = new PaymentResponseViewModel();
            var userId = Functions.User.GetUserId();
            var user = _db.AspNetUsers.FirstOrDefault(w => w.Id == userId);
            PaymentResponse payment = await _paymentClient.GetPaymentAsync(user.UniquePaymentReference);
            if (user.PassThrougReference == guid && user.UniquePaymentReference == payment.Id)
            {
                switch (payment.Status)
                {
                    case Mollie.Api.Models.Payment.PaymentStatus.Paid:
                        //This is our happy path, everything went well, show a successmessage to the user.
                        Subscription subscription = new Subscription()
                        {
                            UserId = Functions.User.GetUserId(),
                            Number = 10,
                            DatePurchased = DateTime.Now,
                            PaymentReference = payment.Id,
                            PaymentMethod = payment.Method.ToString()
                        };
                        Subscriptions.AddSubscription(_db, subscription, Server.MapPath("~\\MailTemplates\\BetalingOntvangen.html"));
                        await _db.SaveChangesAsync();

                        //update user to avoid double subscriptions!
                        var userToUpdate = _db.AspNetUsers.FirstOrDefault(w => w.Id == userId);
                        userToUpdate.UniquePaymentReference = "";
                        userToUpdate.PassThrougReference = "";
                        await _db.SaveChangesAsync();

                        payRes.PassThroughId = guid;
                        payRes.PaymentSucceeded = true;
                        payRes.PaymentReference = user.UniquePaymentReference;
                        payRes.SuccessMessage = "Proficiat, uw betaling is geslaagd! U hebt nu 10 lessen beschikbaar!";
                        break;

                    case Mollie.Api.Models.Payment.PaymentStatus.Open:
                        payRes.ErrorMessage = "Payment is still open, we have to reconnect to mollie";
                        //The payment hasn't started yet, try to relaunch the link to mollie here;
                        break;

                    case Mollie.Api.Models.Payment.PaymentStatus.Pending:
                        payRes.ErrorMessage = "Payment is started, just wait a little...";
                        //The payment has started, wait for it.
                        break;

                    default:
                        //The payment isn't paid, pending or open, we assume it's aborted: propose some actions here
                        payRes.ErrorMessage = "Er is helaas iets misgelopen, klik op onderstaande knop om opnieuw te proberen.";
                        break;
                }

            }
            else
            {
                //the guid we passed to Mollie is not mapped correctly, show an error
                payRes.ErrorMessage = "Er is reeds een nieuwe betaling gestart, u kan dit scherm sluiten.";
            }
            return View(payRes);
           
        }

        [HttpGet]
        public ActionResult Create()
        {   
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(string abc = "")
        {
            if (this.ModelState.IsValid)
            {
                PaymentRequest paymentRequest = new PaymentRequest()
                {
                    Amount = 300,
                    Description = "Aankoop beurtenkaart"
                };
                var guidString = Guid.NewGuid().ToString();

                var urlBuilder =
                new UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("Feedback", "Payment"),
                    Query = "guid=" + guidString

                };

                Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString();

                paymentRequest.RedirectUrl = url;
               
                var paymentResponse = await _paymentClient.CreatePaymentAsync(paymentRequest);

                PaymentResponse payment = await _paymentClient.GetPaymentAsync(paymentResponse.Id);
                var userId = Functions.User.GetUserId();
                var user = _db.AspNetUsers.FirstOrDefault(w => w.Id == userId);
                user.UniquePaymentReference = payment.Id;
                user.PassThrougReference = guidString;
                await _db.SaveChangesAsync();

                return Redirect(payment.Links.PaymentUrl);
            }

            return View();
        }


    }

}