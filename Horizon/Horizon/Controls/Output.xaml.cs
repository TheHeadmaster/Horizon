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
    /// Outputs information to a control in the IDE.
    /// </summary>
    public partial class Output : UserControl
    {
        public static Output Instance { get; private set; }

        public OutputViewModel ViewModel { get; set; }

        public Output()
        {
            this.InitializeComponent();
            Instance = this;
            this.ViewModel = new OutputViewModel();
            this.DataContext = this.ViewModel;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs args)
        {
            this.OutputTextBox.ScrollToEnd();
        }
    }
}