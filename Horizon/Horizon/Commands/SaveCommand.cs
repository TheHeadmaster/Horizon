using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Horizon.Commands
{
    public class SaveCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new SaveCommand();

        public override bool CanExecute(object parameter) => !(App.CurrentProject is null) && !App.InterfaceData.IsRunningStarbound;

        public override void Execute(object parameter)
        {
            App.InterfaceData.StatusState = $"Saving {App.CurrentProject.Name}...";
            App.CurrentProject.Save();
            App.InterfaceData.StatusState = $"{App.CurrentProject.Name} Saved";
        }
    }
}