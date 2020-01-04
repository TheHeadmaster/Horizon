using Horizon;
using Horizon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace Horizon.Commands
{
    public class CloseProjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new CloseProjectCommand();

        public override bool CanExecute(object parameter) => App.CurrentProject.IsNotNull();

        public override void Execute(object parameter)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Changes have been made to {App.CurrentProject.Name}. Would you like to save these changes before closing the project?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                App.CurrentProject.Close(true);
            }
            else if (result == MessageBoxResult.No)
            {
                App.CurrentProject.Close(false);
            }
            else
            {
                return;
            }
        }
    }
}