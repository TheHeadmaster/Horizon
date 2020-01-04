using Horizon.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Horizon.Commands
{
    public class NewProjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new NewProjectCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (App.CurrentProject is null)
            {
                NewProjectWindow wnd = new NewProjectWindow();
                wnd.ShowDialog();
            }
            else
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Changes have been made to {App.CurrentProject.Name}. Would you like to save these changes before closing the project?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    App.CurrentProject.Save();
                    NewProjectWindow wnd = new NewProjectWindow();
                    wnd.ShowDialog();
                }
                else if (result == MessageBoxResult.No)
                {
                    NewProjectWindow wnd = new NewProjectWindow();
                    wnd.ShowDialog();
                }
                else
                {
                    return;
                }
            }
        }
    }
}