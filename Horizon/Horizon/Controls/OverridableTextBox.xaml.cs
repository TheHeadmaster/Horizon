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
    /// Interaction logic for OverridableTextBox.xaml
    /// </summary>
    public partial class OverridableTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty =
DependencyProperty.Register("Text", typeof(string), typeof(OverridableTextBox), new
PropertyMetadata(default(string), new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public OverridableTextBox()
        {
            this.InitializeComponent();
        }

        private static void OnTextChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs args)
        {
            OverridableTextBox overridableTextBoxControl = d as OverridableTextBox;
            overridableTextBoxControl.OnTextChanged(args);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs args)
        {
        }
    }
}