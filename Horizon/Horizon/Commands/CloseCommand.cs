using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Horizon.Commands
{
    /// <summary>
    /// Closes the application.
    /// </summary>
    public class CloseCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new CloseCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}