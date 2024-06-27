using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;

// Declare navigation service interface.  The interface defines methods to get the current page name, navigate to a specific page, or navigate back to the previous page

namespace MyMediaCollection.Interfaces
{
    public interface INavigationService
    {
        string CurrentPage { get; }
        void NavigateTo(string page);
        void NavigateTo(string page, object parameter);
        void GoBack();
    }
}
