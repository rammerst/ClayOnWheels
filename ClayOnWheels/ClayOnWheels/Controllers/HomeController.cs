using System;
using System.Collections.Generic;
using ClayOnWheels.Models;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using ClayOnWheels.Models.EF;

namespace ClayOnWheels.Controllers
{
    public class HomeController : Controller
    {
        private bool _isAdmin;
        private readonly MyDbContext _db = new MyDbContext();

        [Authorize]
        public ActionResult Index()
        {

            ViewBag.TotalSubscriptions = CalculateSubscriptionsForCurrentUser();
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

        public bool SaveHoliday(string NewEventDate)
        {
            return DiaryEvent.CreateHolidayNewEvent(NewEventDate);
        }

        public bool BookWorkshopTemp(int id)
        {
            _db.UserSubscriptions.Add(new UserSubscription
            {
                AppointmentDairyId = id,
                UserId = GetUserId(),
                Created = DateTime.Now,
                Pending = 1
            });
            _db.SaveChangesAsync();
            return true;
        }

        public bool BookWorkshop(int id)
        {
            var total = CalculateSubscriptionsForCurrentUser();
            if (total == 1)
            {
                //todo: Send mail to notify user he/she has to renew subscriptions
            }
            if (total > 0)
            {
                _db.UserSubscriptions.Add(new UserSubscription
                {
                    AppointmentDairyId = id,
                    UserId = GetUserId(),
                    Created = DateTime.Now,
                    Pending = 0
                });
                _db.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public bool RemoveWorkshop(int id)
        {
            try
            {
                var notifyUsers = _db.UserSubscriptions.Where(w => w.AppointmentDairyId == id && w.Pending != 1) ;
                if (notifyUsers.Any())
                {
                    foreach (var user in notifyUsers)
                    {
                        //todo: send mail to user to notify the cursus is cancelled
                    }
                }
                var obj = _db.UserSubscriptions.RemoveRange(notifyUsers);
                var toRemove = _db.AppointmentDiaries.FirstOrDefault(w => w.Id == id);
                _db.AppointmentDiaries.Remove(toRemove);
                _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var exce = ex;

            }
            return false;

        }

        public bool CancelWorkshop(int id)
        {
            try
            {
                var userId = GetUserId();
                var obj = _db.UserSubscriptions.First(w => w.AppointmentDairyId == id && w.UserId == userId);
                _db.UserSubscriptions.Remove(obj);
                _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var exce = ex;

            }
            return false;

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
            var ApptListForDate = DiaryEvent.LoadAllAppointmentsInDateRange(start, end, GetUserId());
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

        public JsonResult GetUsersFromAppointment(int id)
        {
            var results = _db.UserSubscriptions.Where(w => w.AppointmentDairyId == id && w.Pending != 1).ToList();
            return Json(results.Select(res => _db.AspNetUsers.First(w => w.Id == res.UserId)).Select(user => user.FirstName + ' ' + user.LastName).ToArray(), JsonRequestBehavior.AllowGet);
        }
        public int CalculateSubscriptionsForCurrentUser()
        {
            var creditsSum = 0;
            if (ClaimsPrincipal.Current != null)
            {
                _isAdmin = ClaimsPrincipal.Current.IsInRole("Admin");
            }

            if (_isAdmin == false && ClaimsPrincipal.Current != null)
            {
                var userId = GetUserId();

                creditsSum = (from u in _db.Subscriptions where u.UserId == userId select (int?)u.Number).Sum() ?? 0;

                //now substract all bookings
                var bookedSum = _db.UserSubscriptions.Count(u => u.UserId == userId && u.Pending != 1);
                creditsSum -= bookedSum;
            }
            return creditsSum;
        }

        public string GetUserId()
        {
            return ClaimsPrincipal.Current.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
        }

    }
}