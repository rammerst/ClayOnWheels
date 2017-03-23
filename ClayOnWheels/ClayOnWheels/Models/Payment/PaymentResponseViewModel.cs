using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClayOnWheels.Models.Payment
{
    public class PaymentResponseViewModel
    {
        public string PassThroughId  {get;set;}
        public bool PaymentSucceeded { get; set; }
        public string PaymentReference { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }
        
    }
}