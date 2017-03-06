using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;
using ClayOnWheels.Models;

namespace testdbfirst.Controllers
{
    public class UserSubscriptionsController : Controller
    {
        private readonly MyDbContext db = new MyDbContext();

        // GET: UserSubscription
        public ActionResult Index()
        {
            var userSubscriptions = db.UserSubscriptions.Include(u => u.AspNetUser).Include(a => a.AppointmentDiary).Where(u => u.AspNetUser.Active).OrderBy(b => b.AppointmentDiary.DateTimeScheduled).ToList();
            return View(userSubscriptions.ToList());
        }

        // GET: UserSubscription/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var UserSubscription = db.UserSubscriptions.Find(id);
            if (UserSubscription == null)
            {
                return HttpNotFound();
            }
            return View(UserSubscription);
        }

        // GET: UserSubscription/Create
        public ActionResult Create()
        {
            var subscriptions = db.AppointmentDiaries.Where(w => w.StatusEnum != 2 && w.StatusEnum !=3).ToList();
            var users = db.AspNetUsers.Where(w => w.Active).ToList();
            var userItems = from s in users
                       select new SelectListItem
                       {
                           Text = s.FirstName + " - " + s.LastName + '(' + s.Email + ')',
                           Value = s.Id
                       };
            ViewBag.UserId = userItems;
            var item = from s in subscriptions
                       select new SelectListItem
                       {
                           Text = s.DateTimeScheduled + " - " + Enums.GetName((AppointmentStatus)s.StatusEnum),
                           Value = s.Id.ToString()
                       };
            ViewBag.AppointmentDairyId = item;
            return View();
        }

        // POST: UserSubscription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,AppointmentDairyId")] UserSubscription UserSubscription)
        {
            if (ModelState.IsValid)
            {
                UserSubscription.Created = DateTime.Now;
                db.UserSubscriptions.Add(UserSubscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", UserSubscription.UserId);
            return View(UserSubscription);
        }

        // GET: UserSubscription/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubscription UserSubscription = db.UserSubscriptions.Find(id);
            if (UserSubscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", UserSubscription.UserId);
            return View(UserSubscription);
        }

        // POST: UserSubscription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,AppointmentDairyId")] UserSubscription UserSubscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(UserSubscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", UserSubscription.UserId);
            return View(UserSubscription);
        }

        // GET: UserSubscription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubscription UserSubscription = db.UserSubscriptions.Find(id);
            if (UserSubscription == null)
            {
                return HttpNotFound();
            }
            return View(UserSubscription);
        }

        // POST: UserSubscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSubscription UserSubscription = db.UserSubscriptions.Find(id);
            db.UserSubscriptions.Remove(UserSubscription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
