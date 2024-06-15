using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyMediaCollection.Model;
using MyMediaCollection.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WebUI;
using MyMediaCollection.ViewModels;
using System.Runtime.InteropServices;

// This version explicitly updates the interface in relevant handlers via x:Class references to items 

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyMediaCollection
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        // View reference to main ViewModel to bind to (static member of App)
        public MainViewModel ViewModel => App.ViewModel;


        public MainWindow()
        {
            ViewModel.reportEventStatus("MainWindow (before InitialiseComponent...");
            this.InitializeComponent();
            ViewModel.reportEventStatus("MainWindow (after InitialiseComponent...");
        } 

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.reportEventStatus("Add button pressed...");

            var dialog = new ContentDialog
            {
                Title = "My Media Collection",
                Content = "Adding items to the collection is not yet supported.",
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };

            await dialog.ShowAsync();
        }


    }
}
