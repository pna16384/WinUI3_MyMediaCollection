using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Networking.NetworkOperators;

namespace MyMediaCollection.ViewModels
{
    internal class RelayCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecute; // true if null (no conditions) or deletgate returns true to say action can be performed (useful for telling UI actions are not possible and disabling controls accordingly)

        public RelayCommand(Action action) : this(action, null) { }

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            if (action==null)
            {
                throw new ArgumentNullException(nameof(action)); // must have an action to perform
            }

            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute();
        public void Execute(object parameter) => action();

        public event EventHandler CanExecuteChanged;


        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
