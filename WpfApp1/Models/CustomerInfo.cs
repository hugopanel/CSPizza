using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class CustomerInfo
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string TelephoneNumber { get; set; }
        public DateOnly? FirstOrderDate { get; set; }

        public CustomerInfo(string surname, string firstName, string telephoneNumber)
        {
            Surname = surname;
            FirstName = firstName;
            TelephoneNumber = telephoneNumber;
        }

        public CustomerInfo(string surname, string firstName, string telephoneNumber, DateOnly? firstOrderDate) : this(surname, firstName, telephoneNumber)
        {
            FirstOrderDate = firstOrderDate;
        }   
    }
}
