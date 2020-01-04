using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get
            {
                if (App.CurrentProject is null)
                {
                    return "Horizon";
                }
                else
                {
                    return $"Horizon - {App.CurrentProject.Name}";
                }
            }
        }

        public MainWindowViewModel()
        {
            App.PropertyChanged += this.App_PropertyChanged;
        }

        private void App_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(App.CurrentProject))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Title)));
            }
        }
    }
}