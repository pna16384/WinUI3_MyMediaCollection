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
        private IList<MediaItem> _allItems {  get; set; }
        private IList<MediaItem> _items { get; set; }
        private IList<string> _mediums { get; set; }
        private bool _isLoaded;

        public MainWindow()
        {
            this.InitializeComponent();
            ItemList.Loaded += ItemList_Loaded;
            ItemFilter.Loaded += ItemFilter_Loaded;
        }

        private void ItemFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var filterCombo = (ComboBox)sender;
            PopulateData();
            filterCombo.ItemsSource = _mediums;
            filterCombo.SelectedIndex = 0;

            ItemFilter.SelectionChanged += ItemFilter_SelectionChanged;
        }

        private void ItemFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var updatedItems = (from item in _allItems
                                where string.IsNullOrWhiteSpace(ItemFilter.SelectedValue.ToString()) ||
                                ItemFilter.SelectedValue.ToString() == "All" ||
                                ItemFilter.SelectedValue.ToString() == item.MediaType.ToString()
                                select item).ToList();

            ItemList.ItemsSource = updatedItems;
        }

        private void ItemList_Loaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            PopulateData();
            listView.ItemsSource = _items;
        }


        

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "My Media Collection",
                Content = "Adding items to the collection is not yet supported.",
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        /*private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }*/
    }
}
