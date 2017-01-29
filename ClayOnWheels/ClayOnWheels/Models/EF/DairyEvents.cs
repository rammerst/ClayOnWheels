using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization; // << dont forget to add this for converting dates to localtime
using System.Data.Entity.Core.Objects;
using System.Security.Claims;

namespace ClayOnWheels.Models.EF
{
    public class DiaryEvent
    {
        public int ID;
        public string Title;
        public int SomeImportantKeyID;
        public string StartDateString;
        public string EndDateString;
        public string StatusString;
        public string StatusColor;
        public string ClassName;


        public static List<DiaryEvent> LoadAllAppointmentsInDateRange(double start, double end, string userid)
        {
            var isAdmin = false;
            if (ClaimsPrincipal.Current != null)
            {
                isAdmin = ClaimsPrincipal.Current.IsInRole("Admin");
            }
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (var ent = new MyDbContext())
            {
                var rslt = ent.AppointmentDiaries.Where(s => s.DateTimeScheduled >= fromDate && EntityFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate).ToArray();
                var userRslt = ent.UserSubscriptions.Where(s => s.UserId == userid).Select(d => d.AppointmentDairyId).ToArray();
                var result = new List<DiaryEvent>();
                foreach (var item in rslt)
                {
                    var rec = new DiaryEvent
                    {
                        ID = item.Id,
                        StartDateString = item.DateTimeScheduled.ToString("s"),
                        EndDateString = item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"),
                        Title = item.Title + " - " + item.AppointmentLength + " mins"
                    };

                    // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                    // field AppointmentLength is in minutes

                    //when cursist has booked, show event in orange
                    if (userRslt.Contains(item.Id))
                    {
                        rec.StatusString = Enums.GetName((AppointmentStatus)1);
                        rec.SomeImportantKeyID = 666;
                    }
                    else
                    {
                        //when no admin (so cursist) & 24hours before event, show as unavailable = gray
                        if (item.DateTimeScheduled <= DateTime.Now.AddDays(1) && !isAdmin)
                        {
                            rec.StatusString = Enums.GetName((AppointmentStatus)3);
                            rec.SomeImportantKeyID = 3;
                        }
                        else
                        {

                            //check when cursus is fully booked & the current cursist has not booked yet, show in red:
                            var results = ent.UserSubscriptions.Count(w => w.AppointmentDairyId == item.Id);
                            if (results >= 2)
                            {
                                rec.StatusString = Enums.GetName((AppointmentStatus)4);
                                rec.SomeImportantKeyID = 4;
                            }
                            //everything else: show as available or holiday
                            else
                            {
                                rec.StatusString = Enums.GetName((AppointmentStatus)item.StatusEnum);
                                rec.SomeImportantKeyID = item.StatusEnum;
                            }
                        }
                    }

                    rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                    var ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                    rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                    rec.StatusColor = ColorCode;
                    result.Add(rec);
                }

                return result;
            }

        }


        public static List<DiaryEvent> LoadAppointmentSummaryInDateRange(double start, double end)
        {

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            using (var ent = new MyDbContext())
            {
                var rslt = ent.AppointmentDiaries.Where(s => s.DateTimeScheduled >= fromDate && EntityFunctions.AddMinutes(s.DateTimeScheduled, s.AppointmentLength) <= toDate)
                                                        .GroupBy(s => EntityFunctions.TruncateTime(s.DateTimeScheduled))
                                                        .Select(x => new { DateTimeScheduled = x.Key, Count = x.Count() });

                List<DiaryEvent> result = new List<DiaryEvent>();
                int i = 0;
                foreach (var item in rslt)
                {
                    DiaryEvent rec = new DiaryEvent();
                    rec.ID = i; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                    rec.SomeImportantKeyID = -1;
                    string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                    rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                    rec.EndDateString = StringDate + "T23:59:59";
                    rec.Title = "Booked: " + item.Count.ToString();
                    result.Add(rec);
                    i++;
                }

                return result;
            }

        }

        public static void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        {
            // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
            using (var ent = new MyDbContext())
            {
                var rec = ent.AppointmentDiaries.FirstOrDefault(s => s.Id == id);
                if (rec != null)
                {
                    DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
                    rec.DateTimeScheduled = DateTimeStart;
                    if (!String.IsNullOrEmpty(NewEventEnd))
                    {
                        TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
                        rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);
                    }
                    ent.SaveChanges();
                }
            }

        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static bool CreateNewEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration)
        {
            try
            {
                var ent = new MyDbContext();
                var rec = new AppointmentDiary
                {
                    Title = Title,
                    DateTimeScheduled =
                        DateTime.ParseExact(NewEventDate + " " + NewEventTime, "dd/MM/yyyy HH:mm",
                            CultureInfo.InvariantCulture),
                    AppointmentLength = int.Parse(NewEventDuration)
                };
                ent.AppointmentDiaries.Add(rec);
                ent.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool CreateHolidayNewEvent(string NewEventDate)
        {
            try
            {
                var ent = new MyDbContext();
                var rec = new AppointmentDiary
                {
                    Title = "Verlof",
                    DateTimeScheduled =
                        DateTime.ParseExact(NewEventDate + " " + "00:00", "dd/MM/yyyy HH:mm",
                            CultureInfo.InvariantCulture),
                    AppointmentLength = 1440,
                    StatusEnum = 3
                };
                ent.AppointmentDiaries.Add(rec);
                ent.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}