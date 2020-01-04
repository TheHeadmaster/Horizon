using Horizon.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Horizon.Commands
{
    public class OpenPreferencesCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new OpenPreferencesCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            PreferencesWindow wnd = new PreferencesWindow();
            wnd.ShowDialog();
        }
    }
}