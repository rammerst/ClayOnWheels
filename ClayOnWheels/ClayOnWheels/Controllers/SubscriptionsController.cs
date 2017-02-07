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
            var subscriptions = _db.Subscriptions.Include(s => s.AspNetUser);//.Include(s => s.Subscription_Id);
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
            var users = _db.AspNetUsers.Where(w=>w.Active).ToList();
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
        public ActionResult Create([Bind(Include = "Id,UserId,DatePurchased,Number")] Subscription subscriptions)
        {
            if (ModelState.IsValid)
            {
                _db.Subscriptions.Add(subscriptions);
                _db.SaveChanges();

                var id = subscriptions.UserId;
                var s = _db.AspNetUsers.FirstOrDefault(f => f.Id == id);
                if (s != null)
                {
                    var email = s.Email;                  
                    var body = System.IO.File.ReadAllText(Server.MapPath("~\\MailTemplates\\BetalingOntvangen.html"));
                    body = body.Replace("[NAME]", s.FirstName);
                    Mailer.SendEmail(email, "Betaling gelukt bij Clay on Wheels", body);
                }
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_db.AspNetUsers, "Id", "Email", subscriptions.UserId);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscriptions.Id);
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId", subscriptions.Id);
            return View(subscriptions);
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
