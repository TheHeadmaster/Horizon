using Horizon.UI.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Horizon.Controls
{
    public sealed class SlantedBorder : ContentControl
    {
        public static readonly DependencyProperty BottomLeftCornerProperty = DependencyProperty.Register(
            "BottomLeftCorner",
            typeof(CornerType),
            typeof(SlantedBorder),
            new PropertyMetadata(default(CornerType), new PropertyChangedCallback(OnBottomLeftCornerChanged)));

        public static readonly DependencyProperty BottomRightCornerProperty = DependencyProperty.Register(
            "BottomRightCorner",
            typeof(CornerType),
            typeof(SlantedBorder),
            new PropertyMetadata(default(CornerType), new PropertyChangedCallback(OnBottomRightCornerChanged)));

        public static readonly DependencyProperty TopLeftCornerProperty = DependencyProperty.Register(
                            "TopLeftCorner",
            typeof(CornerType),
            typeof(SlantedBorder),
            new PropertyMetadata(default(CornerType), new PropertyChangedCallback(OnTopLeftCornerChanged)));

        public static readonly DependencyProperty TopRightCornerProperty = DependencyProperty.Register(
            "TopRightCorner",
            typeof(CornerType),
            typeof(SlantedBorder),
            new PropertyMetadata(default(CornerType), new PropertyChangedCallback(OnTopRightCornerChanged)));

        public CornerType BottomLeftCorner
        {
            get => (CornerType)this.GetValue(BottomLeftCornerProperty);
            set => this.SetValue(BottomLeftCornerProperty, value);
        }

        public CornerType BottomRightCorner
        {
            get => (CornerType)this.GetValue(BottomRightCornerProperty);
            set => this.SetValue(BottomRightCornerProperty, value);
        }

        public CornerType TopLeftCorner
        {
            get => (CornerType)this.GetValue(TopLeftCornerProperty);
            set => this.SetValue(TopLeftCornerProperty, value);
        }

        public CornerType TopRightCorner
        {
            get => (CornerType)this.GetValue(TopRightCornerProperty);
            set => this.SetValue(TopRightCornerProperty, value);
        }

        public SlantedBorder()
        {
            this.DefaultStyleKey = typeof(SlantedBorder);
        }

        private static void OnBottomLeftCornerChanged(DependencyObject d,
   DependencyPropertyChangedEventArgs args)
        {
            SlantedBorder slantedBorderControl = d as SlantedBorder;
            slantedBorderControl.OnBottomLeftCornerChanged(args);
        }

        private static void OnBottomRightCornerChanged(DependencyObject d,
   DependencyPropertyChangedEventArgs args)
        {
            SlantedBorder slantedBorderControl = d as SlantedBorder;
            slantedBorderControl.OnBottomRightCornerChanged(args);
        }

        private static void OnTopLeftCornerChanged(DependencyObject d,
                           DependencyPropertyChangedEventArgs args)
        {
            SlantedBorder slantedBorderControl = d as SlantedBorder;
            slantedBorderControl.OnTopLeftCornerChanged(args);
        }

        private static void OnTopRightCornerChanged(DependencyObject d,
   DependencyPropertyChangedEventArgs args)
        {
            SlantedBorder slantedBorderControl = d as SlantedBorder;
            slantedBorderControl.OnTopRightCornerChanged(args);
        }

        private void OnBottomLeftCornerChanged(DependencyPropertyChangedEventArgs args)
        {
            Polygon poly = this.Template?.FindName("BottomLeftPoly", this) as Polygon;
            Polyline polyLine = this.Template?.FindName("BottomLeftBorder", this) as Polyline;
            if (poly is null) { return; }
            if (polyLine is null) { return; }
            poly.Points.Clear();
            polyLine.Points.Clear();
            if (this.BottomLeftCorner == CornerType.Slanted)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(24, 24));

                polyLine.Points.Add(new Point(0, 0));
                polyLine.Points.Add(new Point(24, 24));
            }
            else if (this.BottomLeftCorner == CornerType.Square)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(24, 24));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(0, 0));
                polyLine.Points.Add(new Point(0, 24));
                polyLine.Points.Add(new Point(24, 24));
            }
        }

        private void OnBottomRightCornerChanged(DependencyPropertyChangedEventArgs args)
        {
            Polygon poly = this.Template?.FindName("BottomRightPoly", this) as Polygon;
            Polyline polyLine = this.Template?.FindName("BottomRightBorder", this) as Polyline;
            if (poly is null) { return; }
            if (polyLine is null) { return; }
            poly.Points.Clear();
            polyLine.Points.Clear();
            if (this.BottomRightCorner == CornerType.Slanted)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(24, 0));
                polyLine.Points.Add(new Point(0, 24));
            }
            else if (this.BottomRightCorner == CornerType.Square)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(24, 24));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(24, 0));
                polyLine.Points.Add(new Point(24, 24));
                polyLine.Points.Add(new Point(0, 24));
            }
        }

        private void OnTopLeftCornerChanged(DependencyPropertyChangedEventArgs args)
        {
            Polygon poly = this.Template?.FindName("TopLeftPoly", this) as Polygon;
            Polyline polyLine = this.Template?.FindName("TopLeftBorder", this) as Polyline;
            if (poly is null) { return; }
            if (polyLine is null) { return; }
            poly.Points.Clear();
            polyLine.Points.Clear();
            if (this.TopLeftCorner == CornerType.Slanted)
            {
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(24, 24));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(0, 24));
                polyLine.Points.Add(new Point(24, 0));
            }
            else if (this.TopLeftCorner == CornerType.Square)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(24, 24));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(0, 24));
                polyLine.Points.Add(new Point(0, 0));
                polyLine.Points.Add(new Point(24, 0));
            }
        }

        private void OnTopRightCornerChanged(DependencyPropertyChangedEventArgs args)
        {
            Polygon poly = this.Template?.FindName("TopRightPoly", this) as Polygon;
            Polyline polyLine = this.Template?.FindName("TopRightBorder", this) as Polyline;
            if (poly is null) { return; }
            if (polyLine is null) { return; }
            poly.Points.Clear();
            polyLine.Points.Clear();
            if (this.TopRightCorner == CornerType.Slanted)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 24));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(0, 0));
                polyLine.Points.Add(new Point(24, 24));
            }
            else if (this.TopRightCorner == CornerType.Square)
            {
                poly.Points.Add(new Point(0, 0));
                poly.Points.Add(new Point(24, 0));
                poly.Points.Add(new Point(24, 24));
                poly.Points.Add(new Point(0, 24));

                polyLine.Points.Add(new Point(0, 0));
                polyLine.Points.Add(new Point(24, 0));
                polyLine.Points.Add(new Point(24, 24));
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.OnTopLeftCornerChanged(new DependencyPropertyChangedEventArgs(TopLeftCornerProperty, null, this.TopLeftCorner));
            this.OnTopRightCornerChanged(new DependencyPropertyChangedEventArgs(TopRightCornerProperty, null, this.TopRightCorner));
            this.OnBottomLeftCornerChanged(new DependencyPropertyChangedEventArgs(BottomLeftCornerProperty, null, this.BottomLeftCorner));
            this.OnBottomRightCornerChanged(new DependencyPropertyChangedEventArgs(BottomRightCornerProperty, null, this.BottomRightCorner));
        }
    }
}