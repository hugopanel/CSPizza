using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class Cook : Worker
    {
        [JsonConstructor]
        public Cook(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public async Task CookItem(Item item)
        {
            await Task.Delay(item.PreparationTime);
        }
    }
}