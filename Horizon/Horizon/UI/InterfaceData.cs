using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizon.ViewModels;

namespace Horizon.UI
{
    public class InterfaceData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<DocumentControlViewModel> Documents { get; set; } = new ObservableCollection<DocumentControlViewModel>();

        public bool IsRunningStarbound { get; set; } = false;

        public string OutputText { get; set; }

        public string StatusState { get; set; } = "Ready";
    }
}