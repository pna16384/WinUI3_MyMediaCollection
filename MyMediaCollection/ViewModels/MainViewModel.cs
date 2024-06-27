using MyMediaCollection.Enums;
using MyMediaCollection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MyMediaCollection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using MyMediaCollection.Interfaces;

namespace MyMediaCollection.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string selectedMedium;

        [ObservableProperty]
        private ObservableCollection<MediaItem> items = new ObservableCollection<MediaItem>();
        private ObservableCollection<MediaItem> allItems; // private collection of all items ("master" list)

        [ObservableProperty]
        private IList<string> mediums;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteItemCommand))]
        private MediaItem selectedMediaItem;

        private INavigationService _navigationService;
        private IDataService _dataService;

        //private int additionalItemCount = 1;
        private const string AllMediums = "All";
        

        public MainViewModel(INavigationService navigationService, IDataService dataService) 
        {
            _navigationService = navigationService;
            _dataService = dataService;

            PopulateData();
        }


        public void PopulateData()
        {
            // Instead of setting up data here, we pull it in from the data service
            Items.Clear();

            foreach (var item in _dataService.GetItems())
            {
                Items.Add(item);
            }

            allItems = new ObservableCollection<MediaItem>(Items);

            Mediums = new ObservableCollection<string>
            {
                AllMediums
            };

            foreach (var itemType in _dataService.GetItemTypes())
            {
                Mediums.Add(itemType.ToString());
            }

            SelectedMedium = Mediums[0];
        }

        
        // Note: No public properties here - ObservableProperty attribute provides interface for view to be dependent upon


        partial void OnSelectedMediumChanged(string value) // declaration generated as part of ObservableObject (so naming here follows convention!)
        {
            Items.Clear();
            foreach (var item in allItems)
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    value == "All" ||
                    value == item.MediaType.ToString())
                {
                    Items.Add(item);
                }
            }
        }



        // Commands


        [RelayCommand]
        public void AddEditItem()
        {
            var selectedItemId = -1;

            if (SelectedMediaItem != null)
            {
                selectedItemId = SelectedMediaItem.Id;
            }

            _navigationService.NavigateTo("ItemDetailsPage", selectedItemId);
        }

        //public ICommand DeleteCommand { get; set; }

        [RelayCommand(CanExecute = nameof(CanDeleteItem))]
        private void DeleteItem()
        {
            allItems.Remove(SelectedMediaItem);
            Items.Remove(SelectedMediaItem);
        }

        private bool CanDeleteItem() => SelectedMediaItem != null;


        // Double-tapping functionality
        public void ListViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs args)
        {
            AddEditItem();
        }
    }
}
