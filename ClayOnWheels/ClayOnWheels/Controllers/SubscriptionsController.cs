using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;

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
            ViewBag.UserId = new SelectList(_db.AspNetUsers, "Id", "Email");
            ViewBag.Id = new SelectList(_db.Subscriptions, "Id", "UserId");
            return View();
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
            var Subscription = _db.Subscriptions.Find(id);
            _db.Subscriptions.Remove(Subscription);
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
