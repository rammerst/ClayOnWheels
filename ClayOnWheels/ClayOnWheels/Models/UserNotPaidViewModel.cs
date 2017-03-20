using System;

namespace ClayOnWheels.Models
{
    public class UserNotPaidViewModel
    {
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int AantalBeurten { get; set; }
        public DateTime LastBooked { get; set; }
        public DateTime LastLesson { get; set; }
    }
}