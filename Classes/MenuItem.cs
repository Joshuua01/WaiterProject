using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WaiterProject.Classes
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public int MenuItemTypeId { get; set; }
        public MenuItemType MenuItemType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}