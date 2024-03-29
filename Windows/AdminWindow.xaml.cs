﻿using System.Windows;
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
        public UserContext Session = UserContext.getInstance();

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

        private void ManageOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                ManageOrdersWindow manageOrdersWindow = new ManageOrdersWindow();
                manageOrdersWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You don't have permission to access this window");
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
            Session.endSession();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}