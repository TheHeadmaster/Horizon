using Horizon.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Horizon.ObjectModel;
using Horizon.Controls;
using Horizon.Diagnostics;

namespace Horizon.Commands
{
    /// <summary>
    /// Selects an object for the PropertiesView control to inspect.
    /// </summary>
    public class SelectObjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new SelectObjectCommand();

        public override bool CanExecute(object parameter) => true;

        [Log("Changing PropertiesView focus...", ExitMessage = "PropertiesView focus changed.")]
        public override void Execute(object parameter) => PropertiesView.Instance.ViewModel.Focus = parameter;
    }
}