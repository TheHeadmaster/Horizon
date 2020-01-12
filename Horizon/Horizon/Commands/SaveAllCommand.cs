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
    /// Saves all open documents and the current project.
    /// </summary>
    public class SaveAllCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new SaveAllCommand();

        public override bool CanExecute(object parameter)
        {
            if (IDEWindow.Instance is null || Status.Instance is null) { return false; }
            return !(IDEWindow.Instance.ViewModel.CurrentProject is null) && !Status.Instance.ViewModel.IsRunningStarbound;
        }

        [Log("Saving project...", ExitMessage = "Project saved.")]
        public override void Execute(object parameter)
        {
            Status.Instance.ViewModel.ChangeStatus($"Saving {IDEWindow.Instance.ViewModel.CurrentProject.Name}...");
            IDEWindow.Instance.ViewModel.CurrentProject.Save();
            Status.Instance.ViewModel.ChangeStatus($"{IDEWindow.Instance.ViewModel.CurrentProject.Name} Saved");
        }
    }
}