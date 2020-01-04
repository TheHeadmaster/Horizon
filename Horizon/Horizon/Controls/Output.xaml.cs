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
    /// Interaction logic for Output.xaml
    /// </summary>
    public partial class Output : UserControl
    {
        public OutputViewModel ViewModel { get; set; }

        public Output()
        {
            this.InitializeComponent();
            this.ViewModel = new OutputViewModel();
            this.DataContext = this.ViewModel;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.OutputTextBox.ScrollToEnd();
        }
    }
}