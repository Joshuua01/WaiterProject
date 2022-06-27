namespace WaiterProject
{
    public class UserContext
    {
        public User user { get; set; }

        public void endSession()
        {
            user = null;
        }
    }
}