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
    /// Interaction logic for PropertiesView.xaml
    /// </summary>
    public partial class PropertiesView : UserControl
    {
        public static PropertiesView Instance { get; private set; }

        public PropertiesViewModel ViewModel { get; set; }

        public PropertiesView()
        {
            this.InitializeComponent();
            Instance = this;
            this.ViewModel = new PropertiesViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}