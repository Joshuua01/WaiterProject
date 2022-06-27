using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WaiterProject
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}