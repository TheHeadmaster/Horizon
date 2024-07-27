using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace Horizon.View.Controls;

/// <summary>
/// Interaction logic for KeyBindingVisuals.xaml
/// </summary>
public partial class KeyBindingVisuals
{
    public static readonly DependencyProperty KeyBindingTextProperty = DependencyProperty.Register(
    "KeyBindingText", typeof(string),
    typeof(KeyBindingVisuals));

    public KeyBindingVisuals()
    {
        this.InitializeComponent();

        this.ViewModel = new();

        this.WhenActivated(dispose =>
        {
            this.ViewModel.KeyBindingText = this.KeyBindingText;

            this.Bind(this.ViewModel,
                vm => vm.KeyBindingText,
                view => view.KeyBindingText)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.KeyBindings,
                view => view.KeyList.ItemsSource)
            .DisposeWith(dispose);
        });
    }

    public string KeyBindingText
    {
        get => (string)this.GetValue(KeyBindingTextProperty);
        set => this.SetValue(KeyBindingTextProperty, value);
    }
}