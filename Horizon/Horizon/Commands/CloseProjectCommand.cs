using Horizon;
using Horizon.Diagnostics;
using Horizon.Utilities;
using Horizon.Windows;
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
    /// <summary>
    /// Closes the currently open project.
    /// </summary>
    public class CloseProjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new CloseProjectCommand();

        public override bool CanExecute(object parameter) => IDEWindow.Instance.ViewModel.CurrentProject.IsNotNull();

        [Log("Closing project...", ExitMessage = "Project closed successfully.")]
        public override void Execute(object parameter)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Changes have been made to {IDEWindow.Instance.ViewModel.CurrentProject.Name}. Would you like to save these changes before closing the project?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                IDEWindow.Instance.ViewModel.CurrentProject.Close(true);
            }
            else if (result == MessageBoxResult.No)
            {
                IDEWindow.Instance.ViewModel.CurrentProject.Close(false);
            }
            else
            {
                return;
            }
        }
    }
}