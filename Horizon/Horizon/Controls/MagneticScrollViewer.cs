using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Horizon.Controls
{
    public class MagneticScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty IsMagnetizedProperty = DependencyProperty.Register(
        "IsMagnetized",
        typeof(bool),
        typeof(MagneticScrollViewer),
        new PropertyMetadata(false, new PropertyChangedCallback(OnIsMagnetizedChanged)));

        private bool IsInDesignMode
        {
            get
            {
                DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor
                    .FromProperty(prop, typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }

        public bool IsMagnetized
        {
            get => (bool)this.GetValue(IsMagnetizedProperty);
            set => this.SetValue(IsMagnetizedProperty, value);
        }

        public MagneticScrollViewer()
        {
            if (this.IsInDesignMode) { return; }
            Style s = App.Current.FindResource("MagneticScrollViewer") as Style;
            this.Style = s;
            this.ScrollChanged += this.BaseScrollChanged;
        }

        private static void OnIsMagnetizedChanged(DependencyObject d,
       DependencyPropertyChangedEventArgs args)
        {
            MagneticScrollViewer magneticScrollViewerControl = d as MagneticScrollViewer;
            magneticScrollViewerControl.OnIsMagnetizedChanged(args);
        }

        private void BaseScrollChanged(object sender, ScrollChangedEventArgs args)
        {
            ScrollBar s = this.Template.FindName("PART_VerticalScrollBar", this) as ScrollBar;
            if (s.Maximum == 0) { this.IsMagnetized = false; return; }
            if (s.Value == s.Maximum)
            {
                this.IsMagnetized = true;
            }
            else
            {
                if (args.ExtentHeightChange > 0 && this.IsMagnetized)
                {
                    this.ScrollToBottom();
                    return;
                }
                this.IsMagnetized = false;
            }
        }

        private void OnIsMagnetizedChanged(DependencyPropertyChangedEventArgs args)
        {
            ControlTemplate template = App.Current.FindResource("MagneticVerticalScrollBarTemplate") as ControlTemplate;
            ScrollBar s = this.Template.FindName("PART_VerticalScrollBar", this) as ScrollBar;
            Polygon p = template.FindName("MagneticPoly", s) as Polygon;
            if (p is null) { return; }
            if (this.IsMagnetized == true)
            {
                DoubleAnimation animation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.2)), FillBehavior.HoldEnd);
                p.BeginAnimation(OpacityProperty, animation);
            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.2)), FillBehavior.HoldEnd);
                p.BeginAnimation(OpacityProperty, animation);
            }
        }
    }
}