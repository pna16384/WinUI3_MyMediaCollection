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

namespace MyMediaCollection.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MediaItem> items; // data source for list - shows filtered items based on selected medium

        [ObservableProperty]
        private IList<string> mediums;


        [ObservableProperty]
        private string selectedMedium;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteItemCommand))]
        private MediaItem selectedMediaItem;


        private ObservableCollection<MediaItem> allItems; // private collection of all items ("master" list)
        private int additionalItemCount = 1;


        public MainViewModel() 
        {
            PopulateData();
        }


        public void PopulateData()
        {
            var cd = new MediaItem
            {
                Id = 1,
                Name = "Classical Favourites",
                MediaType = ItemType.Music,
                MediumInfo = new Medium
                {
                    Id = 1,
                    MediaType = ItemType.Music,
                    Name = "CD"
                }
            };

            var book = new MediaItem
            {
                Id = 2,
                Name = "Classic Fairy Tales",
                MediaType = ItemType.Book,
                MediumInfo = new Medium
                {
                    Id = 2,
                    MediaType = ItemType.Book,
                    Name = "Book"
                }
            };

            var bluRay = new MediaItem
            {
                Id = 3,
                Name = "The Mummy",
                MediaType = ItemType.Video,
                MediumInfo = new Medium
                {
                    Id = 3,
                    MediaType = ItemType.Video,
                    Name = "Blu Ray"
                }
            };

            // -----------------

            Items = new ObservableCollection<MediaItem> { cd, book, bluRay };
            allItems = new ObservableCollection<MediaItem> { cd, book, bluRay };

            Mediums = new List<string> {
                "All",
                nameof(ItemType.Book),
                nameof(ItemType.Music),
                nameof(ItemType.Video)
            };

            // -----------------

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

        //public ICommand AddEditCommand { get; set; }

        [RelayCommand]
        public void AddEditItem()
        {
            // Note this is temporary until
            // we use a real data source for items.
            const int startingItemCount = 3;

            var newItem = new MediaItem
            {
                Id = startingItemCount + additionalItemCount,
                Location = LocationType.InCollection,
                MediaType = ItemType.Music,
                MediumInfo = new Medium { Id = 1, MediaType = ItemType.Music, Name = "CD" },
                Name = $"CD {additionalItemCount}"
            };

            allItems.Add(newItem);
            Items.Add(newItem);

            additionalItemCount++;
        }

        //public ICommand DeleteCommand { get; set; }

        [RelayCommand(CanExecute = nameof(CanDeleteItem))]
        private void DeleteItem()
        {
            allItems.Remove(SelectedMediaItem);
            Items.Remove(SelectedMediaItem);
        }

        private bool CanDeleteItem() => SelectedMediaItem != null;
    }
}
