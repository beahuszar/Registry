using Registry.Data;
using Registry.DesktopClient.ViewModels;
using Registry.DesktopClient.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace Registry.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //the class that manages the dependency injection for the application
            var container = new UnityContainer();

            //whenever and instance of IBusinessContext is asked, BusinessContext's implementation has to be provided
            container.RegisterType<IBusinessContext, BusinessContext>();

            //ViewModel's constructor is using the IBusinessContext, thus MainViewModel has to be registered too and resolved
            container.RegisterType<MainViewModel>();

            var window = new MainWindow
            {
                DataContext = container.Resolve<MainViewModel>()
            };
            window.ShowDialog();
            
        }
    }
}
