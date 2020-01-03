using Horizon.UI.Presentation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Horizon.Controls
{
    /// <summary>
    /// Displays a list of enabled and disabled mods.
    /// </summary>
    [DoNotNotify]
    public partial class ModListBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty ItemsSourceProperty =
 DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<ModPresentation>), typeof(ModListBox), new
PropertyMetadata(default(ObservableCollection<ModPresentation>), new PropertyChangedCallback(OnItemsSourceChanged)));

        public List<ModPresentation> DisabledMods => this.ItemsSource?.Where(x => x.Enabled = false).ToList();

        public List<ModPresentation> EnabledMods => this.ItemsSource?.Where(x => x.Enabled = true).ToList();

        public ObservableCollection<ModPresentation> ItemsSource
        {
            get => (ObservableCollection<ModPresentation>)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        public ModListBox()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private static void OnItemsSourceChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs args)
        {
            ModListBox modListBoxControl = d as ModListBox;
            modListBoxControl.OnItemsSourceChanged(args);
        }

        private void OnItemsSourceChanged(DependencyPropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnabledMods"));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisabledMods"));
        }
    }
}