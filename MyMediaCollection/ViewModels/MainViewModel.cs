﻿using MyMediaCollection.Enums;
using MyMediaCollection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}