using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
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

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                using (DataContext context = new DataContext())
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
        }

        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            using (DataContext context = new DataContext())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}