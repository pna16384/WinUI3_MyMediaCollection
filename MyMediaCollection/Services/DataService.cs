using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMediaCollection.Enums;
using MyMediaCollection.Interfaces;
using MyMediaCollection.Model;

namespace MyMediaCollection.Services
{
    internal class DataService : IDataService
    {
        // Persisted data
        private IList<ItemType> _itemTypes;
        private IList<Medium> _mediums;
        private IList<LocationType> _locationTypes;
        private IList<MediaItem> _items;
        

        // Constructor
        public DataService()
        {
            PopulateItemTypes();
            PopulateMediums();
            PopulateLocationTypes();
            PopulateItems();
        }


        // Private API

        private void PopulateItemTypes()
        {
            _itemTypes = new List<ItemType>
            {
                ItemType.Book,
                ItemType.Music,
                ItemType.Video
            };
        }

        private void PopulateMediums()
        {
            var cd = new Medium { Id = 1, MediaType = ItemType.Music, Name = "CD" };
            var vinyl = new Medium { Id = 2, MediaType = ItemType.Music, Name = "Vinyl" };
            var hardcover = new Medium { Id = 3, MediaType = ItemType.Book, Name = "Hardcover" };
            var paperback = new Medium { Id = 4, MediaType = ItemType.Book, Name = "Paperback" };
            var dvd = new Medium { Id = 5, MediaType = ItemType.Video, Name = "DVD" };
            var bluRay = new Medium { Id = 6, MediaType = ItemType.Video, Name = "Blu Ray" };

            _mediums = new List<Medium>
            {
                cd,
                vinyl,
                hardcover,
                paperback,
                dvd,
                bluRay
            };
        }

        private void PopulateLocationTypes()
        {
            _locationTypes = new List<LocationType>
            {
                LocationType.InCollection,
                LocationType.Loaned
            };
        }

        // Similar to original but here medium is referened from its own collection;  location of item also added for this version
        public void PopulateItems()
        {
            var cd = new MediaItem
            {
                Id = 1,
                Name = "Classical Favorites",
                MediaType = ItemType.Music,
                MediumInfo = _mediums.FirstOrDefault(m => m.Name == "CD"),
                Location = LocationType.InCollection
            };

            var book = new MediaItem
            {
                Id = 2,
                Name = "Classic Fairy Tales",
                MediaType = ItemType.Book,
                MediumInfo = _mediums.FirstOrDefault(m => m.Name == "Hardcover"),
                Location = LocationType.InCollection
            };

            var bluRay = new MediaItem
            {
                Id = 3,
                Name = "The Mummy",
                MediaType = ItemType.Video,
                MediumInfo = _mediums.FirstOrDefault(m => m.Name == "Blu Ray"),
                Location = LocationType.InCollection
            };

            _items = new List<MediaItem>
            {
                cd,
                book,
                bluRay
            };
        }



        //public int SelectedItemId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // Add item, create and return ID based on number of elements currently in the _items collection
        public int AddItem(MediaItem item)
        {
            item.Id = _items.Max(m => m.Id) + 1;
            _items.Add(item);

            return item.Id;
        }

        public MediaItem GetItem(int id)
        {
            return _items.FirstOrDefault(m => m.Id == id); // return first instance where predicate is true, otherwise default value.
        }

        public IList<MediaItem> GetItems()
        {
            return _items;
        }

        public IList<ItemType> GetItemTypes()
        {
            return _itemTypes;
        }

        public IList<LocationType> GetLocationTypes()
        {
            return _locationTypes;
        }

        public Medium GetMedium(string name)
        {
            return _mediums.FirstOrDefault(m => m.Name == name);
        }

        public IList<Medium> GetMediums()
        {
            return _mediums;
        }

        public IList<Medium> GetMediums(ItemType itemType)
        {
            return _mediums
                .Where(m => m.MediaType == itemType)
                .ToList();
        }

        public void UpdateItem(MediaItem item)
        {
            // Use LINQ to build list and get first item (*** check LINQ docs for what's going on here ***)
            var idx = -1;
            var matchedItem =
                (from x in _items
                 let ind = idx++
                 where x.Id == item.Id
                 select ind).FirstOrDefault();

            if (idx == -1)
            {
                throw new Exception("Unable to update item. Item not found in collection.");
            }

            _items[idx] = item;
        }
    }
}
