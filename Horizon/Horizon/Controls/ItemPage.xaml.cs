using Horizon.ObjectModel;
using Horizon.UI;
using Horizon.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ItemPage.xaml
    /// </summary>
    public partial class ItemPage : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
         DependencyProperty.Register("ViewModel", typeof(ItemViewModel), typeof(ItemPage), new
      PropertyMetadata(default(ItemViewModel), new PropertyChangedCallback(OnViewModelChanged)));

        public ItemViewModel ViewModel
        {
            get => (ItemViewModel)this.GetValue(ViewModelProperty);
            set => this.SetValue(ViewModelProperty, value);
        }

        public ItemPage()
        {
            this.InitializeComponent();
        }

        private static void OnViewModelChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs args)
        {
            ItemPage itemPageControl = d as ItemPage;
            itemPageControl.OnViewModelChanged(args);
        }

        private void OnViewModelChanged(DependencyPropertyChangedEventArgs args)
        {
        }
    }
}