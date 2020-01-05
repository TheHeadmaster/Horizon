using Horizon.Controls;
using Horizon.UI;
using Horizon.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool IsFileButtonEnabled => !Status.Instance.ViewModel.IsRunningStarbound;

        public bool IsTestButtonEnabled => !(IDEWindow.Instance.ViewModel.CurrentProject is null);

        public ObservableCollection<RecentItem> RecentlyOpenedProjects => new ObservableCollection<RecentItem>(App.Metadata.RecentlyOpenedProjects.Reverse());

        public string SaveText => "";// $"Save {App.MainWindow.ActivePage.GetModel().Header}..."; //TODO: Fix whatever this is

        public MainMenuViewModel()
        {
            // TODO: Fix all this horseshit
            //App.PropertyChanged += this.App_PropertyChanged;
            //App.Metadata.PropertyChanged += this.Metadata_PropertyChanged;
            //App.InterfaceData.PropertyChanged += this.UIData_PropertyChanged;
        }

        //private void App_PropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    if (args.PropertyName == nameof(App.CurrentProject))
        //    {
        //        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsTestButtonEnabled)));
        //    }
        //}
        //
        //private void Metadata_PropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    if (args.PropertyName == nameof(App.Metadata.RecentlyOpenedProjects))
        //    {
        //        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(App.Metadata.RecentlyOpenedProjects)));
        //        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(App.Metadata.LastOpenedProject)));
        //    }
        //}
        //
        //private void UIData_PropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    if (args.PropertyName == nameof(App.InterfaceData.IsRunningStarbound))
        //    {
        //        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsFileButtonEnabled)));
        //    }
        //}
    }
}