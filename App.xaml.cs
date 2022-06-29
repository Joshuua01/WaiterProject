using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
