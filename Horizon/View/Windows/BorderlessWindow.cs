using Horizon.Utilities;
using JetBrains.Annotations;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using Button = System.Windows.Controls.Button;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using MouseEventHandler = System.Windows.Input.MouseEventHandler;
using Point = System.Windows.Point;

namespace Horizon.View.Windows;

public class BorderlessWindow : Window
{
#pragma warning disable IDE0052 // Remove unread private members
    [UsedImplicitly] private HwndSource hwndSource = null!;
#pragma warning restore IDE0052 // Remove unread private members

    private bool isManualDrag;

    private bool isMouseButtonDown;

    private Point mouseDownPosition;

    private Point positionBeforeDrag;

    private Point previousScreenBounds;

    public Button CloseButton { get; private set; } = null!;

    public Grid HeaderBar { get; private set; } = null!;

    public double HeightBeforeMaximize { get; private set; } = 0;

    public Grid LayoutRoot { get; private set; } = null!;

    public Button MaximizeButton { get; private set; } = null!;

    public Button MinimizeButton { get; private set; } = null!;

    public WindowState PreviousState { get; private set; }

    public Button RestoreButton { get; private set; } = null!;

    public double WidthBeforeMaximize { get; private set; } = 0;

    public Grid WindowRoot { get; private set; } = null!;

    static BorderlessWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderlessWindow), new FrameworkPropertyMetadata(typeof(BorderlessWindow)));
    }

    public BorderlessWindow()
    {
        double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
        Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
        this.SizeChanged += this.OnSizeChanged;
        this.StateChanged += this.OnStateChanged;
        this.Loaded += this.OnLoaded;
        Rectangle workingArea = screen.WorkingArea;
        this.MaxHeight = (workingArea.Height + 16) / currentDPIScaleFactor;
        SystemEvents.DisplaySettingsChanged += this.SystemEvents_DisplaySettingsChanged;
        this.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseButtonUp), true);
        this.AddHandler(MouseMoveEvent, new MouseEventHandler(this.OnMouseMove));
    }

    public T GetRequiredTemplateChild<T>(string childName) where T : DependencyObject => (T)this.GetTemplateChild(childName);

    public override void OnApplyTemplate()
    {
        this.WindowRoot = this.GetRequiredTemplateChild<Grid>("WindowRoot");
        this.LayoutRoot = this.GetRequiredTemplateChild<Grid>("LayoutRoot");
        this.MinimizeButton = this.GetRequiredTemplateChild<Button>("MinimizeButton");
        this.MaximizeButton = this.GetRequiredTemplateChild<Button>("MaximizeButton");
        this.RestoreButton = this.GetRequiredTemplateChild<Button>("RestoreButton");
        this.CloseButton = this.GetRequiredTemplateChild<Button>("CloseButton");
        this.HeaderBar = this.GetRequiredTemplateChild<Grid>("PART_HeaderBar");

        if (this.LayoutRoot is not null && (this.WindowState == WindowState.Maximized))
        {
            this.LayoutRoot.Margin = this.GetDefaultMarginForDpi();
        }

        if (this.CloseButton is not null)
        {
            this.CloseButton.Click += this.CloseButton_Click;
        }

        if (this.MinimizeButton is not null)
        {
            this.MinimizeButton.Click += this.MinimizeButton_Click;
        }

        if (this.RestoreButton is not null)
        {
            this.RestoreButton.Click += this.RestoreButton_Click;
        }

        if (this.MaximizeButton is not null)
        {
            this.MaximizeButton.Click += this.MaximizeButton_Click;
        }

        this.HeaderBar?.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnHeaderBarMouseLeftButtonDown));

        base.OnApplyTemplate();
    }

    protected virtual Thickness GetDefaultMarginForDpi()
    {
        int currentDPI = SystemHelper.GetCurrentDPI();
        Thickness thickness = new(8, 8, 8, 8);
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
        Thickness thickness = new(7, 7, 5, 7);
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

        Point position = args.GetPosition(this);
        int headerBarHeight = 36;
        int leftmostClickableOffset = 50;

        if ((position.X - this.LayoutRoot.Margin.Left <= leftmostClickableOffset) && (position.Y <= headerBarHeight))
        {
            if (args.ClickCount != 2)
            {
                this.OpenSystemContextMenu(args);
            }
            else
            {
                this.Close();
            }

            args.Handled = true;
            return;
        }

        if ((args.ClickCount == 2) && (this.ResizeMode == ResizeMode.CanResize))
        {
            this.ToggleWindowState();
            return;
        }

        if (this.WindowState == WindowState.Maximized)
        {
            this.isMouseButtonDown = true;
            this.mouseDownPosition = position;
        }
        else
        {
            try
            {
                this.positionBeforeDrag = new Point(this.Left, this.Top);
                this.DragMove();
            }
            catch { }
        }
    }

    protected override void OnInitialized(EventArgs args)
    {
        this.SourceInitialized += this.OnSourceInitialized;
        base.OnInitialized(args);
    }

    protected void ToggleWindowState() => this.WindowState = this.WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal;

    private void CloseButton_Click(object sender, RoutedEventArgs args) => this.Close();

    private void MaximizeButton_Click(object sender, RoutedEventArgs args) => this.ToggleWindowState();

    private void MinimizeButton_Click(object sender, RoutedEventArgs args) => this.WindowState = WindowState.Minimized;

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
        Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
        double width = screen.WorkingArea.Width;
        Rectangle workingArea = screen.WorkingArea;
        this.previousScreenBounds = new Point(width, workingArea.Height);
    }

    private void OnMouseButtonUp(object sender, MouseButtonEventArgs args)
    {
        this.isMouseButtonDown = false;
        this.isManualDrag = false;
        this.ReleaseMouseCapture();
    }

    private void OnMouseMove(object sender, MouseEventArgs args)
    {
        if (!this.isMouseButtonDown)
        {
            return;
        }

        double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
        Point position = args.GetPosition(this);
        Debug.WriteLine(position);
        Point screen = this.PointToScreen(position);
        double x = this.mouseDownPosition.X - position.X;
        double y = this.mouseDownPosition.Y - position.Y;
        if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) > 1)
        {
            double actualWidth = this.mouseDownPosition.X;

            if (this.mouseDownPosition.X <= 0)
            {
                actualWidth = 0;
            }
            else if (this.mouseDownPosition.X >= this.ActualWidth)
            {
                actualWidth = this.WidthBeforeMaximize;
            }

            if (this.WindowState == WindowState.Maximized)
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

    private void OnSizeChanged(object sender, SizeChangedEventArgs args)
    {
        if (this.WindowState == WindowState.Normal)
        {
            this.HeightBeforeMaximize = this.ActualHeight;
            this.WidthBeforeMaximize = this.ActualWidth;
            return;
        }

        if (this.WindowState == WindowState.Maximized)
        {
            Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
            if ((this.previousScreenBounds.X != screen.WorkingArea.Width) || (this.previousScreenBounds.Y != screen.WorkingArea.Height))
            {
                double width = screen.WorkingArea.Width;
                Rectangle workingArea = screen.WorkingArea;
                this.previousScreenBounds = new Point(width, workingArea.Height);
                this.RefreshWindowState();
            }
        }
    }

    private void OnSourceInitialized(object? sender, EventArgs args) => this.hwndSource = (HwndSource)PresentationSource.FromVisual(this);

    private void OnStateChanged(object? sender, EventArgs args)
    {
        Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
        Thickness thickness = new(0);
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
            if ((this.PreviousState == WindowState.Minimized) || ((this.Left == this.positionBeforeDrag.X) && (this.Top == this.positionBeforeDrag.Y)))
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
        Point position = args.GetPosition(this);
        Point screen = this.PointToScreen(position);
        int num = 36;
        if (position.Y < num)
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            IntPtr systemMenu = NativeUtilities.GetSystemMenu(handle, false);
            if (this.WindowState != WindowState.Maximized)
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
        if (this.WindowState == WindowState.Maximized)
        {
            this.ToggleWindowState();
            this.ToggleWindowState();
        }
    }

    private void RestoreButton_Click(object sender, RoutedEventArgs args) => this.ToggleWindowState();

    private void SetMaximizeButtonsVisibility(Visibility maximizeButtonVisibility, Visibility reverseMaximizeButtonVisiility)
    {
        if (this.MaximizeButton is not null)
        {
            this.MaximizeButton.Visibility = maximizeButtonVisibility;
        }

        if (this.RestoreButton is not null)
        {
            this.RestoreButton.Visibility = reverseMaximizeButtonVisiility;
        }
    }

    private void SystemEvents_DisplaySettingsChanged(object? sender, EventArgs args)
    {
        Screen screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
        double width = screen.WorkingArea.Width;
        Rectangle workingArea = screen.WorkingArea;
        this.previousScreenBounds = new Point(width, workingArea.Height);
        this.RefreshWindowState();
    }
}