using Horizon.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace Horizon.Controls
{
    public class BorderlessWindow : Window
    {
        private HwndSource hwndSource;

        private bool isManualDrag;

        private bool isMouseButtonDown;

        private System.Windows.Point mouseDownPosition;

        private System.Windows.Point positionBeforeDrag;

        private System.Windows.Point previousScreenBounds;

        public System.Windows.Controls.Button CloseButton { get; private set; }

        public Grid HeaderBar { get; private set; }

        public double HeightBeforeMaximize { get; private set; }

        public Grid LayoutRoot { get; private set; }

        public System.Windows.Controls.Button MaximizeButton { get; private set; }

        public System.Windows.Controls.Button MinimizeButton { get; private set; }

        public WindowState PreviousState { get; private set; }

        public System.Windows.Controls.Button RestoreButton { get; private set; }

        public double WidthBeforeMaximize { get; private set; }

        public Grid WindowRoot { get; private set; }

        static BorderlessWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderlessWindow), new FrameworkPropertyMetadata(typeof(BorderlessWindow)));
        }

        public BorderlessWindow()
        {
            double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
            Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            base.StateChanged += new EventHandler(this.OnStateChanged);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            Rectangle workingArea = screen.WorkingArea;
            base.MaxHeight = (double)(workingArea.Height + 16) / currentDPIScaleFactor;
            SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEvents_DisplaySettingsChanged);
            this.AddHandler(Window.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseButtonUp), true);
            this.AddHandler(Window.MouseMoveEvent, new System.Windows.Input.MouseEventHandler(this.OnMouseMove));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs args) => this.Close();

        private void MaximizeButton_Click(object sender, RoutedEventArgs args) => this.ToggleWindowState();

        private void MinimizeButton_Click(object sender, RoutedEventArgs args) => this.WindowState = WindowState.Minimized;

        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
            double width = screen.WorkingArea.Width;
            Rectangle workingArea = screen.WorkingArea;
            this.previousScreenBounds = new System.Windows.Point(width, (double)workingArea.Height);
        }

        private void OnMouseButtonUp(object sender, MouseButtonEventArgs args)
        {
            this.isMouseButtonDown = false;
            this.isManualDrag = false;
            this.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs args)
        {
            if (!this.isMouseButtonDown)
            {
                return;
            }

            double currentDPIScaleFactor = (double)SystemHelper.GetCurrentDPIScaleFactor();
            System.Windows.Point position = args.GetPosition(this);
            System.Diagnostics.Debug.WriteLine(position);
            System.Windows.Point screen = base.PointToScreen(position);
            double x = this.mouseDownPosition.X - position.X;
            double y = this.mouseDownPosition.Y - position.Y;
            if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) > 1)
            {
                double actualWidth = this.mouseDownPosition.X;

                if (this.mouseDownPosition.X <= 0)
                {
                    actualWidth = 0;
                }
                else if (this.mouseDownPosition.X >= base.ActualWidth)
                {
                    actualWidth = this.WidthBeforeMaximize;
                }

                if (base.WindowState == WindowState.Maximized)
                {
                    this.ToggleWindowState();
                    this.Top = (screen.Y - position.Y) / currentDPIScaleFactor;
                    this.Left = (screen.X - actualWidth) / currentDPIScaleFactor;
                    this.CaptureMouse();
                }

                this.isManualDrag = true;

                this.Top = (screen.Y - this.mouseDownPosition.Y) / currentDPIScaleFactor;
                this.Left = (screen.X - actualWidth) / currentDPIScaleFactor;
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (base.WindowState == WindowState.Normal)
            {
                this.HeightBeforeMaximize = base.ActualHeight;
                this.WidthBeforeMaximize = base.ActualWidth;
                return;
            }
            if (base.WindowState == WindowState.Maximized)
            {
                Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
                if (this.previousScreenBounds.X != screen.WorkingArea.Width || this.previousScreenBounds.Y != (double)screen.WorkingArea.Height)
                {
                    double width = screen.WorkingArea.Width;
                    Rectangle workingArea = screen.WorkingArea;
                    this.previousScreenBounds = new System.Windows.Point(width, (double)workingArea.Height);
                    this.RefreshWindowState();
                }
            }
        }

        private void OnSourceInitialized(object sender, EventArgs args) => this.hwndSource = (HwndSource)PresentationSource.FromVisual(this);

        private void OnStateChanged(object sender, EventArgs e)
        {
            Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
            Thickness thickness = new Thickness(0);
            if (this.WindowState != WindowState.Maximized)
            {
                double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
                Rectangle workingArea = screen.WorkingArea;
                this.MaxHeight = (workingArea.Height + 16) / currentDPIScaleFactor;
                this.MaxWidth = double.PositiveInfinity;

                if (this.WindowState != WindowState.Maximized)
                {
                    this.SetMaximizeButtonsVisibility(Visibility.Visible, Visibility.Collapsed);
                }
            }
            else
            {
                thickness = this.GetDefaultMarginForDpi();
                if (this.PreviousState == WindowState.Minimized || (this.Left == this.positionBeforeDrag.X && this.Top == this.positionBeforeDrag.Y))
                {
                    thickness = this.GetFromMinimizedMarginForDpi();
                }

                this.SetMaximizeButtonsVisibility(Visibility.Collapsed, Visibility.Visible);
            }

            this.LayoutRoot.Margin = thickness;
            this.PreviousState = this.WindowState;
        }

        private void OpenSystemContextMenu(MouseButtonEventArgs args)
        {
            System.Windows.Point position = args.GetPosition(this);
            System.Windows.Point screen = this.PointToScreen(position);
            int num = 36;
            if (position.Y < num)
            {
                IntPtr handle = new WindowInteropHelper(this).Handle;
                IntPtr systemMenu = NativeUtilities.GetSystemMenu(handle, false);
                if (base.WindowState != WindowState.Maximized)
                {
                    NativeUtilities.EnableMenuItem(systemMenu, 61488, 0);
                }
                else
                {
                    NativeUtilities.EnableMenuItem(systemMenu, 61488, 1);
                }
                int num1 = NativeUtilities.TrackPopupMenuEx(systemMenu, NativeUtilities.TPM_LEFTALIGN | NativeUtilities.TPM_RETURNCMD, Convert.ToInt32(screen.X + 2), Convert.ToInt32(screen.Y + 2), handle, IntPtr.Zero);
                if (num1 == 0)
                {
                    return;
                }

                NativeUtilities.PostMessage(handle, 274, new IntPtr(num1), IntPtr.Zero);
            }
        }

        private void RefreshWindowState()
        {
            if (base.WindowState == WindowState.Maximized)
            {
                this.ToggleWindowState();
                this.ToggleWindowState();
            }
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs args) => this.ToggleWindowState();

        private void SetMaximizeButtonsVisibility(Visibility maximizeButtonVisibility, Visibility reverseMaximizeButtonVisiility)
        {
            if (this.MaximizeButton != null)
            {
                this.MaximizeButton.Visibility = maximizeButtonVisibility;
            }
            if (this.RestoreButton != null)
            {
                this.RestoreButton.Visibility = reverseMaximizeButtonVisiility;
            }
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
            double width = screen.WorkingArea.Width;
            Rectangle workingArea = screen.WorkingArea;
            this.previousScreenBounds = new System.Windows.Point(width, (double)workingArea.Height);
            this.RefreshWindowState();
        }

        protected virtual Thickness GetDefaultMarginForDpi()
        {
            int currentDPI = SystemHelper.GetCurrentDPI();
            Thickness thickness = new Thickness(8, 8, 8, 8);
            if (currentDPI == 120)
            {
                thickness = new Thickness(7, 7, 4, 5);
            }
            else if (currentDPI == 144)
            {
                thickness = new Thickness(7, 7, 3, 1);
            }
            else if (currentDPI == 168)
            {
                thickness = new Thickness(6, 6, 2, 0);
            }
            else if (currentDPI == 192)
            {
                thickness = new Thickness(6, 6, 0, 0);
            }
            else if (currentDPI == 240)
            {
                thickness = new Thickness(6, 6, 0, 0);
            }
            return thickness;
        }

        protected virtual Thickness GetFromMinimizedMarginForDpi()
        {
            int currentDPI = SystemHelper.GetCurrentDPI();
            Thickness thickness = new Thickness(7, 7, 5, 7);
            if (currentDPI == 120)
            {
                thickness = new Thickness(6, 6, 4, 6);
            }
            else if (currentDPI == 144)
            {
                thickness = new Thickness(7, 7, 4, 4);
            }
            else if (currentDPI == 168)
            {
                thickness = new Thickness(6, 6, 2, 2);
            }
            else if (currentDPI == 192)
            {
                thickness = new Thickness(6, 6, 2, 2);
            }
            else if (currentDPI == 240)
            {
                thickness = new Thickness(6, 6, 0, 0);
            }
            return thickness;
        }

        protected virtual void OnHeaderBarMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (this.isManualDrag)
            {
                return;
            }

            System.Windows.Point position = args.GetPosition(this);
            int headerBarHeight = 36;
            int leftmostClickableOffset = 50;

            if (position.X - this.LayoutRoot.Margin.Left <= leftmostClickableOffset && position.Y <= headerBarHeight)
            {
                if (args.ClickCount != 2)
                {
                    this.OpenSystemContextMenu(args);
                }
                else
                {
                    base.Close();
                }
                args.Handled = true;
                return;
            }

            if (args.ClickCount == 2 && this.ResizeMode == ResizeMode.CanResize)
            {
                this.ToggleWindowState();
                return;
            }

            if (base.WindowState == WindowState.Maximized)
            {
                this.isMouseButtonDown = true;
                this.mouseDownPosition = position;
            }
            else
            {
                try
                {
                    this.positionBeforeDrag = new System.Windows.Point(base.Left, base.Top);
                    this.DragMove();
                }
                catch
                {
                }
            }
        }

        protected override void OnInitialized(EventArgs args)
        {
            SourceInitialized += this.OnSourceInitialized;
            base.OnInitialized(args);
        }

        protected void ToggleWindowState()
        {
            if (base.WindowState != WindowState.Maximized)
            {
                base.WindowState = WindowState.Maximized;
            }
            else
            {
                base.WindowState = WindowState.Normal;
            }
        }

        public T GetRequiredTemplateChild<T>(string childName) where T : DependencyObject => (T)this.GetTemplateChild(childName);

        public override void OnApplyTemplate()
        {
            this.WindowRoot = this.GetRequiredTemplateChild<Grid>("WindowRoot");
            this.LayoutRoot = this.GetRequiredTemplateChild<Grid>("LayoutRoot");
            this.MinimizeButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("MinimizeButton");
            this.MaximizeButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("MaximizeButton");
            this.RestoreButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("RestoreButton");
            this.CloseButton = this.GetRequiredTemplateChild<System.Windows.Controls.Button>("CloseButton");
            this.HeaderBar = this.GetRequiredTemplateChild<Grid>("PART_HeaderBar");

            if (this.LayoutRoot != null && this.WindowState == WindowState.Maximized)
            {
                this.LayoutRoot.Margin = this.GetDefaultMarginForDpi();
            }

            if (this.CloseButton != null)
            {
                this.CloseButton.Click += this.CloseButton_Click;
            }

            if (this.MinimizeButton != null)
            {
                this.MinimizeButton.Click += this.MinimizeButton_Click;
            }

            if (this.RestoreButton != null)
            {
                this.RestoreButton.Click += this.RestoreButton_Click;
            }

            if (this.MaximizeButton != null)
            {
                this.MaximizeButton.Click += this.MaximizeButton_Click;
            }

            if (this.HeaderBar != null)
            {
                this.HeaderBar.AddHandler(Grid.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnHeaderBarMouseLeftButtonDown));
            }

            base.OnApplyTemplate();
        }
    }
}