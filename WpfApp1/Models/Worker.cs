using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public abstract class Worker
    {
        private static int IdCount = 0;


        // Assign the value of IdCount and increment the global counter by one.
        private int _id = IdCount++; 
        
        /// <summary>
        /// The identifier of the worker.
        /// </summary>
        public int Id { get => _id; }
        
        /// <summary>
        /// The name of the worker.
        /// </summary>
        public string Name { get; set; }

        public Address? Address { get; set; }

        public int NbOrders { get; set; }


        public void CheckIn()
        {
            throw new NotImplementedException();
        }

        public void CheckOut()
        {
            throw new NotImplementedException();
        }
    }
}
