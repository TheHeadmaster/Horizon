using Horizon.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    /// <summary>
    /// The ViewModel for the Output control.
    /// </summary>
    public class OutputViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string OutputText { get; set; }
    }
}