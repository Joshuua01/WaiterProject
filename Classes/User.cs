using System;
using System.ComponentModel.DataAnnotations;
using WaiterProject.Classes;

namespace WaiterProject
{
    public class User : IEquatable<User>
    {
        [Key]
        public int UserId { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public bool Equals(User other)
        {
            return Login == other.Login;
        }
    }
}