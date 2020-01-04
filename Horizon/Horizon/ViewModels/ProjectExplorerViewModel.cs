using Horizon.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Horizon.ViewModels
{
    public class ProjectExplorerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Project CurrentProject => App.CurrentProject;

        public Visibility RootNodeVisibility
        {
            get
            {
                if (App.CurrentProject is null)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public ProjectExplorerViewModel()
        {
            App.PropertyChanged += this.App_PropertyChanged;
        }

        private void App_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(App.CurrentProject))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CurrentProject)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.RootNodeVisibility)));
            }
        }
    }
}