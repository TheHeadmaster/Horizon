using PropertyChanged;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ToggleSlider.xaml
    /// </summary>
    [DoNotNotify]
    public partial class ToggleSlider : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly DependencyProperty rectAWidthProperty =
                DependencyProperty.Register("RectAWidth", typeof(double), typeof(ToggleSlider), new
PropertyMetadata(default(double), new PropertyChangedCallback(OnRectAWidthChanged)));

        private static readonly DependencyProperty rectBWidthProperty =
DependencyProperty.Register("RectBWidth", typeof(double), typeof(ToggleSlider), new
PropertyMetadata(default(double), new PropertyChangedCallback(OnRectBWidthChanged)));

        public static readonly DependencyProperty TextLeftProperty =
                    DependencyProperty.Register("TextLeft", typeof(string), typeof(ToggleSlider), new
    PropertyMetadata(default(string), new PropertyChangedCallback(OnTextLeftChanged)));

        public static readonly DependencyProperty TextRightProperty =
    DependencyProperty.Register("TextRight", typeof(string), typeof(ToggleSlider), new
    PropertyMetadata(default(string), new PropertyChangedCallback(OnTextRightChanged)));

        public static readonly DependencyProperty ValueProperty =
                            DependencyProperty.Register("Value", typeof(bool), typeof(ToggleSlider), new
            PropertyMetadata(default(bool), new PropertyChangedCallback(OnValueChanged)));

        private double RectAWidth
        {
            get => (double)this.GetValue(rectAWidthProperty);

            set
            {
                this.SetValue(rectAWidthProperty, value);
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RectA"));
            }
        }

        private double RectBWidth
        {
            get => (double)this.GetValue(rectBWidthProperty);

            set
            {
                this.SetValue(rectBWidthProperty, value);
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RectB"));
            }
        }

        public int RectA => Convert.ToInt32((this.Width / 2) * this.RectAWidth);

        public int RectB => Convert.ToInt32((this.Width / 2) * this.RectBWidth);

        public string TextLeft
        {
            get => (string)this.GetValue(TextLeftProperty);
            set => this.SetValue(TextLeftProperty, value);
        }

        public string TextRight
        {
            get => (string)this.GetValue(TextRightProperty);
            set => this.SetValue(TextRightProperty, value);
        }

        public bool Value
        {
            get => (bool)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        public ToggleSlider()
        {
            this.InitializeComponent();
            this.DataContext = this;
            if (this.Value)
            {
                this.RectAWidth = 0;
                this.RectBWidth = 1;
            }
            else
            {
                this.RectAWidth = 1;
                this.RectBWidth = 0;
            }
        }

        private static void OnRectAWidthChanged(DependencyObject d,
   DependencyPropertyChangedEventArgs args)
        {
            ToggleSlider toggleSliderControl = d as ToggleSlider;
            toggleSliderControl.OnRectAWidthChanged(args);
        }

        private static void OnRectBWidthChanged(DependencyObject d,
   DependencyPropertyChangedEventArgs args)
        {
            ToggleSlider toggleSliderControl = d as ToggleSlider;
            toggleSliderControl.OnRectBWidthChanged(args);
        }

        private static void OnTextLeftChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs args)
        {
            ToggleSlider toggleSliderControl = d as ToggleSlider;
            toggleSliderControl.OnTextLeftChanged(args);
        }

        private static void OnTextRightChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs args)
        {
            ToggleSlider toggleSliderControl = d as ToggleSlider;
            toggleSliderControl.OnTextRightChanged(args);
        }

        private static void OnValueChanged(DependencyObject d,
                                   DependencyPropertyChangedEventArgs args)
        {
            ToggleSlider toggleSliderControl = d as ToggleSlider;
            toggleSliderControl.OnValueChanged(args);
        }

        private void Button_Click(object sender, RoutedEventArgs args) => this.Value = !this.Value;

        private void OnRectAWidthChanged(DependencyPropertyChangedEventArgs args) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RectA"));

        private void OnRectBWidthChanged(DependencyPropertyChangedEventArgs args) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RectB"));

        private void OnTextLeftChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        private void OnTextRightChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs args)
        {
        }
    }
}