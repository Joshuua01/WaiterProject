using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using WaiterProject.Classes;

namespace WaiterProject
{
    /// <summary>
    /// Logika interakcji dla klasy EditUsersPanel.xaml
    /// </summary>
    public partial class ManageUsersWindow : Window
    {
        private const Role WINDOW_ROLE = Classes.Role.Admin;
        public UserContext Session = UserContext.getInstance();

        public ManageUsersWindow()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = Enum.GetValues(typeof(Role));
            Read();
        }

        private string HashPassword(string password)
        {
            var sha = SHA256.Create();

            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashedPassword);
        }

        public void Read()
        {
            using (DataContext context = new DataContext())
            {
                var users = context.Users.ToList();
                UserList.ItemsSource = users;
            }
        }

        public void Create()
        {
            using (DataContext context = new DataContext())
            {
                if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
                {
                    if (LoginTextBox.Text != "" && PasswordTextBox.Password != "" && RoleComboBox.SelectedItem != null)
                    {
                        var user = new User();
                        user.Login = LoginTextBox.Text;
                        user.Password = HashPassword(PasswordTextBox.Password);
                        user.Role = (Role)RoleComboBox.SelectedValue;
                        context.Users.Add(user);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateException exception)
                        {
                            MessageBox.Show("Could not add user");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You are not admin");
                }
            }
        }

        public void Update()
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (Session.user.UserId == ((User)UserList.SelectedItem).UserId)
                {
                    MessageBox.Show("You can't edit your own account");
                    return;
                }
                using (DataContext context = new DataContext())
                {
                    if (UserList.SelectedItem != null)
                    {
                        var user = context.Users.FirstOrDefault(u => u.UserId == ((User)UserList.SelectedItem).UserId);
                        user.Login = LoginTextBox.Text;
                        user.Password = HashPassword(PasswordTextBox.Password);
                        user.Role = (Role)RoleComboBox.SelectedValue;
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateException exception)
                        {
                            MessageBox.Show("Wrong data");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You are not admin");
            }
        }

        public void Delete()
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (Session.user.UserId == ((User)UserList.SelectedItem).UserId)
                {
                    MessageBox.Show("You can't delete your own account");
                    return;
                }
                using (DataContext context = new DataContext())
                {
                    if (UserList.SelectedItem != null)
                    {
                        var user = context.Users.FirstOrDefault(u => u.UserId == ((User)UserList.SelectedItem).UserId);
                        context.Users.Remove(user);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateException exception)
                        {
                            MessageBox.Show("Could not delete user");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You are not admin");
            }
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (LoginTextBox.Text != "" && PasswordTextBox.Password != "" && RoleComboBox.SelectedItem != null)
                {
                    Create();
                    Read();
                }
                else
                {
                    MessageBox.Show("Fill all fields");
                }
            }
            else
            {
                MessageBox.Show("You are not admin");
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (LoginTextBox.Text != "" && PasswordTextBox.Password != "" && RoleComboBox.SelectedItem != null)
                {
                    Update();
                    Read();
                }
                else
                {
                    MessageBox.Show("Fill all fields");
                }
            }
            else
            {
                MessageBox.Show("You are not admin");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (RoleComboBox.SelectedItem != null)
                {
                    Delete();
                    Read();
                }
                else
                {
                    MessageBox.Show("Fill all fields");
                }
            }
            else
            {
                MessageBox.Show("You are not admin");
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Session.endSession();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }
    }
}