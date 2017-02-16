using System;
using ClayOnWheels.Models;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using ClayOnWheels.Functions;
using ClayOnWheels.Models.EF;
using log4net;
namespace ClayOnWheels.Controllers
{


    public class HomeController : Controller
    {
        private bool _isAdmin;
        private readonly MyDbContext _db = new MyDbContext();
        private static readonly ILog logger = LogManager.GetLogger(typeof(HomeController));


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
            if (ClaimsPrincipal.Current != null)
            {
                DiaryEvent.UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
            }
        }

        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventTimeEnd)
        {
            if (ClaimsPrincipal.Current != null)
            {
                return DiaryEvent.CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventTimeEnd);
            }
            return false;
        }

        public bool SaveHoliday(string NewEventDate)
        {
            if (ClaimsPrincipal.Current != null)
            {
                return DiaryEvent.CreateHolidayNewEvent(NewEventDate);
            }
            return false;
        }

        public bool BookWorkshopTemp(int id)
        {
            if (ClaimsPrincipal.Current != null)
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
            return false;
        }

        public bool BookWorkshop(int id)
        {
            if (ClaimsPrincipal.Current != null)
            {
                var total = CalculateSubscriptionsForCurrentUser();
                var sendMail = total == 1;
                if (total > 0)
                {
                    _db.UserSubscriptions.Add(new UserSubscription
                    {
                        AppointmentDairyId = id,
                        UserId = GetUserId(),
                        Created = DateTime.Now,
                        Pending = 0
                    });
                    _db.SaveChanges();
                    var userId = GetUserId();
                    var user = _db.AspNetUsers.FirstOrDefault(w => w.Id == userId);
                    if (user != null && sendMail)
                    {
                        var body = System.IO.File.ReadAllText(Server.MapPath("~\\MailTemplates\\BeurtenOp.html"));
                        body = body.Replace("[NAME]", user.FirstName);
                        Mailer.SendEmail(user.Email, "Aanvraag nieuwe beurtenkaart van Clay on Wheels", body);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool RemoveWorkshop(int id, bool withReplacement = false)
        {
            if (ClaimsPrincipal.Current != null)
            {
                try
                {
                    using (var ent = new MyDbContext())
                    {
                        var notifyUsers = ent.UserSubscriptions.Where(w => w.AppointmentDairyId == id && w.Pending != 1).ToList();
                        var userIds = notifyUsers.Select(d => d.UserId).ToArray();
                        ent.UserSubscriptions.RemoveRange(notifyUsers);
                        var toRemove = ent.AppointmentDiaries.FirstOrDefault(w => w.Id == id);
                        if (toRemove != null)
                        {
                            var dateToUser = toRemove.DateTimeScheduled;
                            if (withReplacement)
                            {
                                toRemove.Title = "Annulering les";
                                toRemove.StatusEnum = 2;
                            }
                            else
                            {
                                ent.AppointmentDiaries.Remove(toRemove);
                            }
                            ent.SaveChanges();

                            if (userIds.Any())
                            {
                                foreach (var user in userIds)
                                {
                                    var userInfo = ent.AspNetUsers.FirstOrDefault(w => w.Id == user);
                                    if (userInfo != null)
                                    {
                                        var body =
                                            System.IO.File.ReadAllText(
                                                Server.MapPath("~\\MailTemplates\\LesGeannuleerd.html"));
                                        body = body.Replace("[NAME]", userInfo.FirstName);
                                        body = body.Replace("[DATECANCELLED]",
                                            dateToUser.ToString("dd/MM/yyyy HH:mm"));
                                        Mailer.SendEmail(userInfo.Email,
                                            "Les uitzonderlijk geannuleerd Clay on Wheels", body);
                                    }
                                }
                            }
                         
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error("Exception in RemoveWorkshop", ex);
                    return false;

                }
            }
            return false;
        }

        public bool CancelWorkshop(int id)
        {
            if (ClaimsPrincipal.Current != null)
            {
                try
                {
                    var userId = GetUserId();
                    var obj = _db.UserSubscriptions.First(w => w.AppointmentDairyId == id && w.UserId == userId);
                    _db.UserSubscriptions.Remove(obj);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error("Exception in RemoveWorkshop", ex);
                    return false;

                }
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
                                textKey = e.OriginalKeyID,
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
                                textKey = e.OriginalKeyID,
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

        public static string GetUserId()
        {
            var id = ClaimsPrincipal.Current.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
            return id;
        }

    }
}