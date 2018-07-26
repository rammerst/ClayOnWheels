using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;
using System.Linq;
using ClayOnWheels.Functions;

namespace ClayOnWheels.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubscriptionsController : Controller
    {
        private readonly MyDbContext _db = new MyDbContext();

        // GET: Subscriptions
        public ActionResult Index()
        {
            var subscriptions = _db.Subscriptions.Include(s => s.AspNetUser).Where(w => w.DatePurchased.Year == 2018).OrderByDescending(w => w.DatePurchased);//.Include(s => s.Subscription_Id);
            return View(subscriptions);
        }

        // GET: Subscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subscriptions = _db.Subscriptions.Find(id);
            if (subscriptions == null)
            {
                return HttpNotFound();
            }
            return View(subscriptions);
        }

        // GET: Subscriptions/Create
        public ActionResult Create()
        {
            var users = _db.AspNetUsers.Where(w => w.Active).ToList();
            var item = from s in users
                       select new SelectListItem
                       {
                           Text = s.FirstName + " - " + s.LastName + '(' + s.Email + ')',
                           Value = s.Id
                       };
            ViewBag.UserId = item;
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId");
            var sub = new Subscription()
            {
                Number = 10,
                DatePurchased = DateTime.Now
            };
            return View(sub);
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,DatePurchased,Number")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                subscription.PaymentReference = "";
                subscription.PaymentMethod = "Manueel";
                Subscriptions.AddSubscription(_db, subscription, Server.MapPath("~\\MailTemplates\\BetalingOntvangen.html"));
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_db.AspNetUsers, "Id", "Email", subscription.UserId);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscription.Id);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscription.Id);
            return View(subscription);
        }

        // GET: Subscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subscriptions = _db.Subscriptions.Find(id);
            if (subscriptions == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(_db.AspNetUsers, "Id", "Email", subscriptions.UserId);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscriptions.Id);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscriptions.Id);
            return View(subscriptions);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,DatePurchased,Number")] Subscription subscriptions)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(subscriptions).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_db.AspNetUsers, "Id", "Email", subscriptions.UserId);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscriptions.Id);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscriptions.Id);
            return View(subscriptions);
        }

        // GET: Subscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subscriptions = _db.Subscriptions.Find(id);
            if (subscriptions == null)
            {
                return HttpNotFound();
            }
            return View(subscriptions);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var subscription = _db.Subscriptions.Find(id);
            _db.Subscriptions.Remove(subscription);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
