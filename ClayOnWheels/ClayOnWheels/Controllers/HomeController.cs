using ClayOnWheels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;
using RestSharp;
using RestSharp.Authenticators;

namespace ClayOnWheels.Controllers
{
    public class HomeController : Controller
    {
        private bool _isAdmin;

        [Authorize]
        public ActionResult Index()
        {

            if (ClaimsPrincipal.Current != null)
            {
                _isAdmin = ClaimsPrincipal.Current.IsInRole("Admin");
            }
            ViewBag.isAdmin = _isAdmin;
            return View();
        }
        public string Init()
        {
            bool rslt = Utils.InitialiseDiary();
            return rslt.ToString();
        }

        public void UpdateEvent(int id, string NewEventStart, string NewEventEnd)
        {
            DiaryEvent.UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
        }


        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration)
        {
            return DiaryEvent.CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration);
        }

        public JsonResult GetDiarySummary(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = DiaryEvent.LoadAllAppointmentsInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ContactPost()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api",
                   ReadSetting("MAILGUN_API_KEY"))
            };
            var request = new RestRequest();
            request.AddParameter("domain", ReadSetting("MAILGUN_DOMAIN"), ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Myriam Thas <mailgun@" + ReadSetting("MAILGUN_DOMAIN") + ">");
            request.AddParameter("to", "rammerst@gmail.com");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            var x = client.Execute(request);

            return View("Contact");
        }
        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return "";
        }
    }
}