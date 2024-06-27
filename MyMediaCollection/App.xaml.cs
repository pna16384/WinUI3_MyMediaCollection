using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MyMediaCollection.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MyMediaCollection.Views;
using MyMediaCollection.Services;
using MyMediaCollection.Interfaces;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyMediaCollection
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        // Host (service) container
        public static IHost HostContainer { get; private set; }

        private Window m_window;

        
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.RequestedTheme = ApplicationTheme.Light;
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // MainWindow XAML is empty so create a new frame containing MainPage and set this as the main window's content instead!
            m_window = new MainWindow();
            var rootFrame = new Frame();

            RegisterComponents(rootFrame); // Setup host and services so accessible when we create the window...

            rootFrame.NavigationFailed += RootFrame_NavigationFailed;
            rootFrame.Navigate(typeof(MainPage), args);
            m_window.Content = rootFrame;

            m_window.Activate();
        }

        private void RegisterComponents(Frame rootFrame)
        {
            // Create navigation service and add pages for main view and itemdetails view
            var navigationService = new NavigationService(rootFrame);

            navigationService.Configure(nameof(MainPage), typeof(MainPage));
            navigationService.Configure(nameof(ItemDetailsPage), typeof(ItemDetailsPage));

            var dataService = new DataService();

            HostContainer = Host.CreateDefaultBuilder().ConfigureServices(
                services => {
                    services.AddSingleton<INavigationService>(navigationService); // pre-created and initialised service object
                    //services.AddSingleton<IDataService, DataService>(); // created when added???
                    //
                    
                    services.AddSingleton<IDataService>(dataService);
                    services.AddTransient<MainViewModel>(); // MainViewModel constructor will get passed the service objects by the container when instantiated :)
                    services.AddTransient<ItemDetailsViewModel>();
                }
            ).Build();
        }


        // Event handling

        private void RootFrame_NavigationFailed(object sender,
  NavigationFailedEventArgs e)
        {
            throw new Exception($"Error loading page {e.SourcePageType.FullName}");
        }


    }
}
