using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace WaiterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserContext session = new UserContext();

        public MainWindow()
        {
            InitializeComponent();
            using (DataContext context = new DataContext())
            {
                User admin = new User
                {
                    Login = "admin",
                    Password = "admin",
                    Role = "Admin"
                };
                context.Add(admin);
                User user = new User
                {
                    Login = "user",
                    Password = "user",
                    Role = "Waiter"
                };
                context.Add(user);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                }
            }
            Read();
        }

        public void Read()
        {
            using (DataContext context = new DataContext())
            {
                var users = context.Users.ToList();
                UserList.ItemsSource = users;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == LoginTextBox.Text && u.Password == PasswordTextBox.Text);
                if (user != null)
                {
                    if (user.Role == (string)RoleComboBox.SelectedValue && user.Role == "Admin")
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else if (user.Role == (string)RoleComboBox.SelectedValue && user.Role == "Waiter")
                    {
                        WaiterWindow waiterWindow = new WaiterWindow();
                        waiterWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong role", "Login Alert!");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong login or password", "Login Alert!");
                }
                session.user = user;
            }
        }
    }
}