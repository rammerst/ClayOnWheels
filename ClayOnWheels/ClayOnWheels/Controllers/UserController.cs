using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;

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
            return View(list);
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
