using System;
using WaiterProject.Classes;

namespace WaiterProject
{
    public class UserContext
    {
        private static UserContext Instance;

        public User user { get; set; }
        
        public static UserContext getInstance()
        {
            if (Instance == null)
            {
                Instance = new UserContext();
            }
            return Instance;
        }

        public Boolean UserRoleValidation(Role role)
        {
            return user.Role == role;
        }

        public void endSession()
        {
            Instance = new UserContext();
        }
    }
}