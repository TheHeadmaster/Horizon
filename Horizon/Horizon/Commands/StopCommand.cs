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

namespace Horizon.Commands
{
    public class StopCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new StopCommand();

        public override bool CanExecute(object parameter) => App.InterfaceData.IsRunningStarbound == true;

        public override void Execute(object parameter) => PlayCommand.StarboundRunning.Kill();
    }
}