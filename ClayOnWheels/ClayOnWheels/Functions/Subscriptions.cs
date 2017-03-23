using ClayOnWheels.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClayOnWheels.Functions
{
    public static class Subscriptions
    {
        public static void AddSubscription(MyDbContext _db, Subscription sub, string pathToTemplate )
        {
            _db.Subscriptions.Add(sub);
            _db.SaveChanges();

            var id = sub.UserId;
            var s = _db.AspNetUsers.FirstOrDefault(f => f.Id == id);
            if (s != null)
            {
                var email = s.Email;
                var body = System.IO.File.ReadAllText(pathToTemplate);
                body = body.Replace("[NAME]", s.FirstName);
                Mailer.SendEmail(email, "Betaling gelukt bij Clay on Wheels", body);
            }
        }
    }
}