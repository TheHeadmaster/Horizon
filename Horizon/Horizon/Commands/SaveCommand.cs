using Horizon.Controls;
using Horizon.Diagnostics;
using Horizon.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Horizon.Commands
{
    /// <summary>
    /// Saves the current document.
    /// </summary>
    public class SaveCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new SaveCommand();

        public override bool CanExecute(object parameter)
        {
            if (IDEWindow.Instance is null || Status.Instance is null) { return false; }
            return !(IDEWindow.Instance.ViewModel.CurrentProject is null) && !Status.Instance.ViewModel.IsRunningStarbound;
        }

        [Log("Saving document...", ExitMessage = "Document saved.")]
        public override void Execute(object parameter)
        {
            // TODO: Fix this to save the current document only.
            // App.InterfaceData.StatusState = $"Saving {App.CurrentProject.Name}...";
            //App.CurrentProject.Save();
            // App.InterfaceData.StatusState = $"{App.CurrentProject.Name} Saved";
        }
    }
}