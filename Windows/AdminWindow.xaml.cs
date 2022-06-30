using System.Windows;
using WaiterProject.Classes;
using WaiterProject.Windows;

namespace WaiterProject
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private const Role WINDOW_ROLE = Classes.Role.Admin;

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ManageUsersButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                ManageUsersWindow manageUsersWindow = new ManageUsersWindow();
                manageUsersWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You don't have permission to access this window");
            }
        }

        private void ManageMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                ManageMenuWindow manageMenuWindow = new ManageMenuWindow();
                manageMenuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You don't have permission to access this window");
            }
        }

        /*
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
        }*/
    }
}