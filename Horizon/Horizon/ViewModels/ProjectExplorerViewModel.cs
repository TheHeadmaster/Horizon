using Horizon.ObjectModel;
using Horizon.Windows;
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

        public Project CurrentProject => IDEWindow.Instance.ViewModel.CurrentProject;

        public Visibility RootNodeVisibility
        {
            get
            {
                if (IDEWindow.Instance.ViewModel.CurrentProject is null)
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
            IDEWindow.Instance.ViewModel.PropertyChanged += this.App_PropertyChanged;
        }

        private void App_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(IDEWindow.Instance.ViewModel.CurrentProject))
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CurrentProject)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.RootNodeVisibility)));
            }
        }
    }
}