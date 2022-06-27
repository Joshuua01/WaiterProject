using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace WaiterProject
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            Read();
        }

        public void Read()
        {
            using (DataContext context = new DataContext())
            {
                var users = context.Users.ToList();
                UserListAdmin.ItemsSource = users;
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                User user = new User
                {
                    Login = "admin1",
                    Password = "admin",
                    Role = "Admin"
                };
                context.Add(user);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    MessageBox.Show("User already exists");
                }
            }
            Read();
        }
    }
}