using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterProject.Classes
{
    
    public class MenuItemType
    {
        [Key]
        public int MenuItemTypeId { get; set; }
        public ItemType Name { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
