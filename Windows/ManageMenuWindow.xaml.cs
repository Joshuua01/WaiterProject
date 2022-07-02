using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using WaiterProject.Classes;

namespace WaiterProject.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy ManageMenuWindow.xaml
    /// </summary>
    public partial class ManageMenuWindow : Window
    {
        private const Role WINDOW_ROLE = Classes.Role.Admin;
        public UserContext Session = UserContext.getInstance();

        public ManageMenuWindow()
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(ItemType));
            Read();
        }

        public void Read()
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                using (DataContext context = new DataContext())
                {
                    var menuItems = context.MenuItems
                        .Include(s => s.MenuItemType)
                        .ToList();
                    MenuList.ItemsSource = menuItems;
                }
            }
        }

        public void Create()
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                if (NameTextBox.Text != "" && PriceTextBox.Text != "" && TypeComboBox.SelectedValue != null)
                {
                    try
                    {
                        using (DataContext context = new DataContext())
                        {
                            Classes.MenuItem menuItem = new Classes.MenuItem
                            {
                                Name = NameTextBox.Text,
                                Price = Convert.ToDouble(PriceTextBox.Text),
                                MenuItemType = context.MenuItemTypes.FirstOrDefault(m => m.Name == (ItemType)TypeComboBox.SelectedValue),
                            };
                            context.Add(menuItem);

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (DbUpdateException exception)
                            {
                                MessageBox.Show("Could not add menu item");
                            }
                        }
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show("Invalid price format");
                    }
                }
            }
        }

        public void Update()
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                using (DataContext context = new DataContext())
                {
                    if (MenuList.SelectedItem != null)
                    {
                        if (NameTextBox.Text != "" && PriceTextBox.Text != "" && TypeComboBox.SelectedValue != null)
                        {
                            var item = context.MenuItems.FirstOrDefault(m => m.MenuItemId == ((MenuItem)MenuList.SelectedItem).MenuItemId);
                            item.Name = NameTextBox.Text;
                            item.Price = Convert.ToDouble(PriceTextBox.Text);
                            item.MenuItemType = context.MenuItemTypes.FirstOrDefault(m => m.Name == (ItemType)TypeComboBox.SelectedValue);
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
        }

        public void Delete()
        {
            if (UserContext.getInstance().UserRoleValidation(WINDOW_ROLE))
            {
                using (DataContext context = new DataContext())
                {
                    if (MenuList.SelectedItem != null)
                    {
                        var item = context.MenuItems.FirstOrDefault(m => m.MenuItemId == ((MenuItem)MenuList.SelectedItem).MenuItemId);
                        context.MenuItems.Remove(item);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateException exception)
                        {
                            MessageBox.Show("Select item to delete");
                        }
                    }
                }
            }
        }

        private void AddMenuItemButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
            Read();
        }

        private void EditMenuItemButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
            Read();
        }

        private void DeleteMenuItemButton_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            Read();
        }

        private void RefreshMenuItemListButton_Click(object sender, RoutedEventArgs e)
        {
            Read();
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