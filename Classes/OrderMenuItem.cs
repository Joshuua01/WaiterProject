namespace WaiterProject.Classes
{
    public class OrderMenuItem
    {
        public int MenuItemId { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}