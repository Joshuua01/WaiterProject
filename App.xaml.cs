using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using WaiterProject.Classes;

namespace WaiterProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade db = new DatabaseFacade(new DataContext());
            db.EnsureCreated();

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
            
            using (DataContext context = new DataContext())
            {
                foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                {
                    context.Add(new MenuItemType { Name = itemType });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException exception)
                    {
                    }
                }
            }

            using (DataContext context = new DataContext())
            {
                Classes.MenuItem menuItem1 = new Classes.MenuItem
                {
                    Name = "Pizza",
                    Price = 10.25,
                    MenuItemType = context.MenuItemTypes.FirstOrDefault(m => m.Name == ItemType.MainCourse)
                };
                Classes.MenuItem menuItem2 = new Classes.MenuItem
                {
                    Name = "Sushi",
                    Price = 10.25,
                    MenuItemType = context.MenuItemTypes.FirstOrDefault(m => m.Name == ItemType.MainCourse)
                };
                context.MenuItems.Add(menuItem1);
                context.MenuItems.Add(menuItem2);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                }
            }
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            /*using (DataContext context = new DataContext())
            {
                context.Database.EnsureDeleted();
            }*/
        }
    }
}