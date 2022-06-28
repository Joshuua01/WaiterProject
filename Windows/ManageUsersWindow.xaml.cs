using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
                    if (LoginTextBox.Text != "" && PasswordTextBox.Text != "" && RoleComboBox.SelectedItem != null)
                    {
                        var user = new User();
                        user.Login = LoginTextBox.Text;
                        user.Password = PasswordTextBox.Text;
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
                        user.Password = PasswordTextBox.Text;
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
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (LoginTextBox.Text != "" && PasswordTextBox.Text != "" && RoleComboBox.SelectedItem != null)
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
                if (LoginTextBox.Text != "" && PasswordTextBox.Text != "" && RoleComboBox.SelectedItem != null)
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
    }
}