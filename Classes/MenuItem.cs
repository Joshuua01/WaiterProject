using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WaiterProject.Classes
{
    public class MenuItem : IEquatable<MenuItem>
    {
        [Key]
        public int MenuItemId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public int MenuItemTypeId { get; set; }
        public MenuItemType MenuItemType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public bool Equals(MenuItem other)
        {
            return this.MenuItemId.Equals(other.MenuItemId);
        }
    }
}