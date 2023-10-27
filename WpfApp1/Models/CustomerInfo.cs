using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace WpfApp1.Models
{
    public class CustomerInfo : INotifyPropertyChanged
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string TelephoneNumber { get; set; }
        public DateOnly? FirstOrderDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomerInfo(string surname, string firstName, string telephoneNumber)
        {
            Surname = surname;
            FirstName = firstName;
            TelephoneNumber = telephoneNumber;
        }

        [JsonConstructor]

        public CustomerInfo(string surname, string firstName, string telephoneNumber, DateOnly? firstOrderDate) : this(surname, firstName, telephoneNumber)
        {
            FirstOrderDate = firstOrderDate;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override string ToString()
        {
            return $"Surname: {Surname}, FirstName: {FirstName}, Telephone number: {TelephoneNumber}, First Order Date: {FirstOrderDate?.ToString() ?? "N/A"}";
        }
    }
}
