using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using WaiterProject.Classes;

namespace WaiterProject.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy ManageOrdersWindow.xaml
    /// </summary>
    public partial class ManageOrdersWindow : Window
    {
        public UserContext Session = UserContext.getInstance();

        public ManageOrdersWindow()
        {
            InitializeComponent();
            Read();
        }

        public void Read()
        {
            using (DataContext context = new DataContext())
            {
                var orders = context.Orders.ToList();
                ManageOrdersList.ItemsSource = orders;
                var menuItems = context.MenuItems
                .Include(s => s.MenuItemType)
                .ToList();
                MenuList.ItemsSource = menuItems;
            }
        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                Order order = new Order();

                order.OrderDate = DateTime.Now;
                order.OrderStatus = "Open";
                order.TotalPrice = 0;
                order.WaiterName = Session.user.Login;

                context.Orders.Add(order);

                context.SaveChanges();
            }
            Read();
        }

        private void ChangeOrderStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var order = (Order)ManageOrdersList.SelectedItem;
            using (DataContext context = new DataContext())
            {
                var fetchOrder = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (fetchOrder.OrderStatus == "Open")
                {
                    fetchOrder.OrderStatus = "Closed";
                }
                else
                {
                    fetchOrder.OrderStatus = "Open";
                }
                context.SaveChanges();
            }
            Read();
        }

        private void AddItemOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var order = (Order)ManageOrdersList.SelectedItem;
            var item = (MenuItem)MenuList.SelectedItem;

            using (DataContext context = new DataContext())
            {
                var fetchOrder = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                fetchOrder.MenuItems.Add(item);
                fetchOrder.TotalPrice += item.Price;
                context.SaveChanges();
            }
            Read();
        }

        public void ManageOrdersList_Selected(object sender, RoutedEventArgs e)
        {
            var order = (Order)ManageOrdersList.SelectedItem;

            using (DataContext context = new DataContext())
            {
                var orderList = context.Orders
                    .Where(o => o.OrderId == order.OrderId)
                    .Include(o => o.MenuItems)
                    .FirstOrDefaultAsync();

                ManageOrderItemsList.ItemsSource = orderList.Result.MenuItems.ToList();
            }
        }
    }
}