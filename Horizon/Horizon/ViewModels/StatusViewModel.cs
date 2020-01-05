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

        public bool IsRunningStarbound { get; set; } = false;

        public Color StatusBarColor
        {
            get
            {
                if (this.IsRunningStarbound)
                {
                    return Color.FromRgb(202, 81, 0);
                }
                else
                {
                    return Color.FromRgb(0, 122, 204);
                }
            }
        }

        public string StatusState { get; set; } = "Ready";
    }
}