using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Horizon.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Color StatusBarColor
        {
            get
            {
                if (App.InterfaceData.IsRunningStarbound)
                {
                    return Color.FromRgb(202, 81, 0);
                }
                else
                {
                    return Color.FromRgb(0, 122, 204);
                }
            }
        }

        public string StatusBarStateText => App.InterfaceData.StatusState;

        public StatusViewModel()
        {
            App.InterfaceData.PropertyChanged += this.UIData_PropertyChanged;
        }

        private void UIData_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(App.InterfaceData.IsRunningStarbound))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.StatusBarColor)));
            }
            else if (args.PropertyName == nameof(App.InterfaceData.StatusState))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.StatusBarStateText)));
            }
        }
    }
}