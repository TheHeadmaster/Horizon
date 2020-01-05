using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Horizon.Commands
{
    /// <summary>
    /// Handles the RelayCommand design pattern.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Predicate<T> canExecute;

        private readonly Action<T> execute;

        public RelayCommand(Action<T> execute)
           : this(execute, null)
        {
            this.execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => this.canExecute == null || this.canExecute((T)parameter);

        public void Execute(object parameter) => this.execute((T)parameter);
    }

    /// <summary>
    /// Handles the RelayCommand design pattern.
    /// </summary>
    public abstract class RelayCommand : ICommand
    {
        private event EventHandler CanExecuteChangedInternal;

        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChangedInternal.Raise(this);
    }
}