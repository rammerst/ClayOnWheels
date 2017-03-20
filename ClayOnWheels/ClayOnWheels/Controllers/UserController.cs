﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;
using System.Collections.Generic;
using System;

namespace ClayOnWheels.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: User
        public ActionResult Index()
        {
            var list = db.AspNetUsers.ToList();
            ViewBag.TotalActive = list.Count(w => w.Active);
            foreach (var item in list)
            {
                var creditsSum = 0;
                creditsSum = (from u in db.Subscriptions where u.UserId == item.Id select (int?)u.Number).Sum() ?? 0;

                //now substract all bookings
                var bookedSum = db.UserSubscriptions.Count(u => u.UserId == item.Id && u.Pending != 1);
                creditsSum -= bookedSum;

                item.AccessFailedCount = creditsSum;
            }
            return View(list);
        }

        public ActionResult UsersWithNoSubscriptionsLeft()
        {
            var list = db.AspNetUsers.Where(a => a.Active).ToList();
            var resultList = new List<AspNetUser>();
            ViewBag.TotalActive = list.Count(w => w.Active);
            foreach (var item in list)
            {
                var creditsSum = 0;
                creditsSum = (from u in db.Subscriptions where u.UserId == item.Id select (int?)u.Number).Sum() ?? 0;

                //now substract all bookings
                var bookedSum = db.UserSubscriptions.Count(u => u.UserId == item.Id && u.Pending != 1);
                creditsSum -= bookedSum;

                item.AccessFailedCount = creditsSum;

                if (creditsSum <= 0)
                {
                    var max = db.UserSubscriptions.Where(u => u.UserId == item.Id && u.Pending != 1).Max(m => m.Created);                   
                    var maxLes = db.UserSubscriptions.Where(u => u.UserId == item.Id && u.Pending != 1).Max(m => m.AppointmentDiary.DateTimeScheduled);
                    item.SecurityStamp = max.Value.ToString("d/M/yyyy");
                    item.LockoutEndDateUtc = maxLes; 
                    resultList.Add(item);
                }
            }
            resultList = resultList.OrderBy(o => o.LockoutEndDateUtc).ToList();
            return View(resultList);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser AspNetUser = db.AspNetUsers.Find(id);
            if (AspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(AspNetUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Address,City,PostalCode")] AspNetUser aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUsers);
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName,Address,City,PostalCode,Active")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                var joske = db.AspNetUsers.FirstOrDefault(w => w.Id == aspNetUser.Id);
                if (joske != null)
                {
                    joske.Email = aspNetUser.Email;
                    joske.PhoneNumber = aspNetUser.PhoneNumber;
                    joske.PostalCode = aspNetUser.PostalCode;
                    joske.FirstName = aspNetUser.FirstName;
                    joske.LastName = aspNetUser.LastName;
                    joske.UserName = aspNetUser.UserName;
                    joske.Address = aspNetUser.Address;
                    joske.Active = aspNetUser.Active;
                    joske.City = aspNetUser.City;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(aspNetUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
