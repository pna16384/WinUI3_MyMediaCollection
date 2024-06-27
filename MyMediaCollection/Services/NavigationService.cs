using Microsoft.UI.Xaml.Controls;
using MyMediaCollection.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMediaCollection.Services
{
    class NavigationService : INavigationService
    {
        private static Frame AppFrame; // Singleton - store reference to frame which will host pages service manages

        private readonly IDictionary<string, Type> _pages = new ConcurrentDictionary<string, Type>();

        public const string RootPage = "(Root)";
        public const string UnknownPage = "(Unknown)";

        public NavigationService(Frame rootFrame) // Constructor
        {
            AppFrame = rootFrame;
        }


        // Configure used to ensure single view type added to container
        public void Configure(string page, Type type)
        {
            // if type is already stored throw an exception
            if (_pages.Values.Any(v => v == type))
            {
                throw new ArgumentException($"The {type.Name} view has already been registered under another name.");
            }

            _pages[page] = type;
        }


        // INavigationService implementation

        // Return the name of the current page...
        public string CurrentPage
        {
            get
            {
                var frame = AppFrame;

                // If at very start page return (Root) name string defined above
                if (frame.BackStackDepth == 0)
                    return RootPage;

                // If the frame contains no page then return (Unknown) name string defined above
                if (frame.Content == null)
                    return UnknownPage;

                // If frame contains a view and its not the root, get the type of page contained in the frame...
                var type = frame.Content.GetType();

                // if ALL values in the _pages dict don't contain the current type, return (Unknown) page string
                if (_pages.Values.All(v => v != type))
                    return UnknownPage;

                // Given configure guards against multiple page types being stored, we now get the SINGLE page name in the dict (shouldn't happen, but an exception will be thrown if we have >1 instance of a given type in _pages).
                var item = _pages.Single(i => i.Value == type);

                // Once we have the item with the given page type, we return the key (which is the name of the stored page type in the dict).
                return item.Key;
            }
        }

        public void NavigateTo(string page) // pass-through to NavigateTo overload below...
        {
            NavigateTo(page, null);
        }

        public void NavigateTo(string page, object parameter)
        {
            // IF we haven't got a page with key 'page' then throw.
            if (!_pages.ContainsKey(page))
            {
                throw new ArgumentException($"Unable to find a page registered with the name {page}.");
            }

            // Otherwise, get host frame to navigate to stored page type.
            AppFrame.Navigate(_pages[page], parameter);
        }

        public void GoBack()
        {
            if (AppFrame?.CanGoBack == true)
            {
                AppFrame.GoBack();
            }
        }
    }
}
