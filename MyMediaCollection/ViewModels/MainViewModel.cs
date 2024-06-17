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

namespace MyMediaCollection.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string selectedMedium;
        private ObservableCollection<MediaItem> items;
        private ObservableCollection<MediaItem> allItems;
        private IList<string> mediums;
        
        private MediaItem selectedMediaItem;
        private int additionalItemCount = 1;

        public MainViewModel() 
        {

            Debug.Console.write("MainViewModel constructor\n");

            if (this.eventSet())
            {
                Debug.Console.write("PropertyChanged event set!!!\n\n\n");
            }
            else
            {
                Debug.Console.write("PropertyChanged event STILL NULL!!!\n\n\n");
            }

            PopulateData();

            // Setup commands
            DeleteCommand = new RelayCommand(DeleteItem, CanDeleteItem);
            AddEditCommand = new RelayCommand(AddOrEditItem); // don't need CanDeleteItem param as Add is always available
        }

        public void reportEventStatus(string prefixText)
        {
            string reportText;

            if (this.eventSet())
            {
                reportText = "PropertyChanged SET\n\n";
            }
            else
            {
                reportText = "PropertyChanged STILL NULL\n\n";
            }

            Debug.Console.write(prefixText + reportText);
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

            items = new ObservableCollection<MediaItem> { cd, book, bluRay };
            allItems = new ObservableCollection<MediaItem> { cd, book, bluRay };

            // -----------------

            mediums = new List<string> {
                "All",
                nameof(ItemType.Book),
                nameof(ItemType.Music),
                nameof(ItemType.Video)
            };

            // -----------------

            selectedMedium = mediums[0];
        }

        
        // Public properties

        public ObservableCollection<MediaItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                SetProperty(ref items, value); // null property name so update everything :)
            }
        }

        public IList<string> Mediums
        {
            get
            {
                return mediums;
            }
            set
            {
                SetProperty(ref mediums, value);
            }
        }

        public string SelectedMedium
        {
            get
            {
                return selectedMedium;
            }
            set
            {
                SetProperty(ref selectedMedium, value);

                Items.Clear();

                foreach (var item in allItems)
                {
                    if (string.IsNullOrWhiteSpace(selectedMedium) ||
                        selectedMedium == "All" ||
                        selectedMedium == item.MediaType.ToString())
                    {
                        Items.Add(item);
                    }
                }
            }
        }

        public MediaItem SelectedMediaItem
        {
            get => selectedMediaItem;
            set
            {
                SetProperty(ref selectedMediaItem, value);
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }


        // Commands

        public ICommand AddEditCommand { get; set; }
        public void AddOrEditItem()
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

        public ICommand DeleteCommand { get; set; }
        private void DeleteItem()
        {
            allItems.Remove(SelectedMediaItem);
            Items.Remove(SelectedMediaItem);
        }

        private bool CanDeleteItem() => selectedMediaItem != null;
    }
}
