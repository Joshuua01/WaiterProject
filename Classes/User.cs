using System.ComponentModel.DataAnnotations;
using WaiterProject.Classes;

namespace WaiterProject
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}