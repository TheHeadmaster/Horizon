using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class OutputViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string OutputText => App.InterfaceData.OutputText;

        public OutputViewModel()
        {
            App.InterfaceData.PropertyChanged += this.UIData_PropertyChanged;
        }

        private void UIData_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(App.InterfaceData.OutputText))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.OutputText)));
            }
        }
    }
}