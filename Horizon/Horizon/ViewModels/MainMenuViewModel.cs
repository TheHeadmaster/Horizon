using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    /// <summary>
    /// ViewModel for main menu control.
    /// </summary>
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public bool IsFileButtonEnabled => !App.InterfaceData.IsRunningStarbound;
    }
}