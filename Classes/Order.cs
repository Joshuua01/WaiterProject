using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WaiterProject.Classes
{
    public class Order
    {
        public Order()
        {
            MenuItems = new List<MenuItem>();
        }

        [Key]
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string WaiterName { get; set; }
        public double TotalPrice { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}