using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.UI
{
    public class LauncherMeta : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string LauncherPath { get; set; } = @"C:\Archives\Development\Erisa's Starbound";
    }
}