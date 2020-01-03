using Horizon.UI.Presentation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        public List<ModPresentation> DisabledMods => this.ItemsSource?.Where(x => x.Enabled == false).ToList();

        public List<ModPresentation> EnabledMods => this.ItemsSource?.Where(x => x.Enabled == true).ToList();

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
            if (!(args.OldValue is null))
            {
                ObservableCollection<ModPresentation> oldValue = args.OldValue as ObservableCollection<ModPresentation>;
                oldValue.CollectionChanged -= this.OnItemsSourceCollectionChanged;
            }
            if (!(args.NewValue is null))
            {
                ObservableCollection<ModPresentation> newValue = args.NewValue as ObservableCollection<ModPresentation>;
                newValue.CollectionChanged += this.OnItemsSourceCollectionChanged;
            }
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnabledMods"));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisabledMods"));
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnabledMods"));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisabledMods"));
        }

        private void ToggleEnableButtonClick(object sender, RoutedEventArgs args)
        {
            Button button = sender as Button;
            ContentPresenter presenter = button.TemplatedParent as ContentPresenter;
            ModPresentation mod = presenter.Content as ModPresentation;
            mod.Enabled = !mod.Enabled;

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnabledMods"));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisabledMods"));
        }
    }
}