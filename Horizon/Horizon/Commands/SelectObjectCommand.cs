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

namespace Horizon.Commands
{
    public class SelectObjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new SelectObjectCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => PropertiesView.Instance.ViewModel.Focus = parameter;
    }
}