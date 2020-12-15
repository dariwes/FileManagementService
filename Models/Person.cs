using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public int ID;
        public string PersonType   { get; set; }
        public string FirstName    { get; set; }
        public string MiddleName   { get; set; }
        public string LastName     { get; set; }
        public int? EmailPromotion { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber  { get; set; }
        public string PasswordSalt { get; set; }
        public string City         { get; set; }
    }
}
