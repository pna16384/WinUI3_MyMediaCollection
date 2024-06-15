using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // Needed for INotifyPropertyChanged
using System.Runtime.CompilerServices;

namespace MyMediaCollection.ViewModels
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // nullable (init to null)
    

        // Inherited and accessible from ViewModels derived from BindableBase
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // If above PropertyChanged event set, invoke it an and pass property changed name as event arg
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected bool SetProperty<T>(ref T originalValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(originalValue, newValue))
            {
                return false;
            }

            originalValue = newValue;

            OnPropertyChanged(propertyName); // call above method to invoke PropertyChanged event

            return true;
        }


    }
}
