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
using Horizon.Windows;
using Horizon.Diagnostics;

namespace Horizon.Commands
{
    /// <summary>
    /// Stops the current running instance of Starbound.
    /// </summary>
    public class StopCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new StopCommand();

        public override bool CanExecute(object parameter) => App.InterfaceMeta.IsRunningStarbound == true;

        [Log("Stopping Starbound instance...", ExitMessage = "Starbound instance stopped.")]
        public override void Execute(object parameter) => PlayCommand.StarboundRunning.Kill();
    }
}