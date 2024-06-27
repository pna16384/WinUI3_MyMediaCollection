using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyMediaCollection.ViewModels;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyMediaCollection.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemDetailsPage : Page
    {
        // This page talks to the ItemDetailsViewModel...
        public ItemDetailsViewModel ViewModel;

        public ItemDetailsPage()
        {
            ViewModel = App.HostContainer.Services.GetService<ItemDetailsViewModel>();
            this.InitializeComponent();
        }


        // Override OnNavigatedTo to recieve parameter from original page we're coming FROM...
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var itemId = (int)e.Parameter;

            if (itemId > 0)
            {
                ViewModel.InitializeItemDetailData(itemId); // populate viewmodel based on item id passed in
            }
        }
    }
}
