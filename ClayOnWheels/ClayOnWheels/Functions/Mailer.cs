using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ClayOnWheels.Functions
{
    public class Mailer
    {
        public static void SendErrorEmail(Exception ex)
        {
            try
            {
                // Command line argument must the the SMTP host.
                var client = new SmtpClient("smtp.telenet.be", 587)
                {
                    Credentials = new System.Net.NetworkCredential("myriam.thas@telenet.be", ReadSetting("mail")),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                // Specify the e-mail sender.
                // Create a mailing address that includes a UTF8 character
                // in the display name.
                var from = new MailAddress("myriam.thas@telenet.be", "Clayonwheels", System.Text.Encoding.UTF8);
                // Set destinations for the e-mail message.
                var to = new MailAddress("brambarnard@gmail.com");

                // Specify the message content.
                var message = new MailMessage(from, to)
                {
                    Body = ex.ToString(),
                    IsBodyHtml = true,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    Subject = "Clay on wheels - Unexpected error occured",
                    SubjectEncoding = System.Text.Encoding.UTF8
                };
                
                // Set the method that is called back when the send operation ends.
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                // The userState can be any object that allows your callback 
                // method to identify this send operation.
                // For this example, the userToken is a string constant.
                var userState = "test message1";
                client.Send(message);

                // Clean up.
                message.Dispose();
                //Console.WriteLine("Goodbye.");
            }
            catch (Exception exc)
            {
                //eat exc
            }
        }
        public static void SendEmail(string sendTo, string subject, string body)
        {
            try
            {
                // Command line argument must the the SMTP host.
                var client = new SmtpClient("smtp.telenet.be", 587);
                client.Credentials = new System.Net.NetworkCredential("myriam.thas@telenet.be", ReadSetting("mail"));
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                // Specify the e-mail sender.
                // Create a mailing address that includes a UTF8 character
                // in the display name.
                var from = new MailAddress("myriam.thas@telenet.be", "Clayonwheels", System.Text.Encoding.UTF8);
                // Set destinations for the e-mail message.
                var to = new MailAddress(sendTo);
                
                // Specify the message content.
                var message = new MailMessage(from, to)
                {
                    Body = body,
                    IsBodyHtml = true,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    Subject = subject,
                    SubjectEncoding = System.Text.Encoding.UTF8
                   // CC = new MailAddress("clayonwheels@telenet.be")
                };
                var copy = new MailAddress("myriam.thas@gmail.com");
                message.Bcc.Add(copy);


                // Set the method that is called back when the send operation ends.
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                // The userState can be any object that allows your callback 
                // method to identify this send operation.
                // For this example, the userToken is a string constant.
                var userState = "test message1";
               // client.Send(message);

                // Clean up.
                message.Dispose();
                //Console.WriteLine("Goodbye.");
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                //trace("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                //Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                //Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                //Tracing.("Error reading app settings");
            }
            return "";
        }
    }
}

