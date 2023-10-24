using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public abstract class Worker : INotifyPropertyChanged
    {
        public static int IdCount = 0;

        /// <summary>
        /// The identifier of the worker.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The name of the worker.
        /// </summary>
        public string Name { get; set; }

        public string? Address { get; set; }

        public int NbOrders { get; set; }

        public void CheckIn()
        {
            throw new NotImplementedException();
        }

        public void CheckOut()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
