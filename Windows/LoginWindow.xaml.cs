using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using WaiterProject.Classes;

namespace WaiterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserContext Session = UserContext.getInstance();

        public MainWindow()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>();
            using (DataContext context = new DataContext())
            {
                User admin = new User
                {
                    Login = "admin",
                    Password = "admin",
                    Role = Classes.Role.Admin
                };
                context.Add(admin);
                User user = new User
                {
                    Login = "user",
                    Password = "user",
                    Role = Classes.Role.Waiter
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
                    if (user.Role == (Role)RoleComboBox.SelectedValue &&  Classes.Role.Admin == user.Role )
                    { 
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else if (user.Role == (Role)RoleComboBox.SelectedValue && Classes.Role.Waiter == user.Role)
                    {
                        WaiterWindow waiterWindow = new WaiterWindow();
                        waiterWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong role", "Login Alert!");
                    }
                    Session.user = user;
                }
                else
                {
                    MessageBox.Show("Wrong login or password", "Login Alert!");
                }
                
            }
        }
    }
}