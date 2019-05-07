using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClayOnWheels;
using ClayOnWheels.Functions;
using Xunit;

namespace ClayOnWheels.Test.Functions
{
    public class MailerTest
    {
        [Fact]
        public void SendMail()
        {
            ClayOnWheels.Functions.Mailer.SendEmail("brambarnard@gmail.com", "testmail Clay On Wheels", "Gewoon een test");
        }
    }
}
