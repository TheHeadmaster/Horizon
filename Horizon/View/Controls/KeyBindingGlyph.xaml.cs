using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Input;

namespace Horizon.View.Controls;

/// <summary>
/// Interaction logic for KeyBindingGlyph.xaml
/// </summary>
public partial class KeyBindingGlyph
{
    public static readonly DependencyProperty KeyBindingProperty = DependencyProperty.Register(
    "KeyBinding", typeof(Key),
    typeof(KeyBindingGlyph));

    public KeyBindingGlyph()
    {
        this.InitializeComponent();

        this.ViewModel = new();

        this.WhenActivated(dispose =>
        {
            this.ViewModel.GlyphKey = this.KeyBinding;

            this.OneWayBind(this.ViewModel,
                vm => vm.GlyphImage,
                view => view.BindingImage.Source)
            .DisposeWith(dispose);

            this.Bind(this.ViewModel,
                vm => vm.Height,
                view => view.Height)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.Width,
                view => view.Width)
            .DisposeWith(dispose);

            this.Bind(this.ViewModel,
                vm => vm.GlyphKey,
                view => view.KeyBinding)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.GlyphText,
                view => view.BindingText.Content)
            .DisposeWith(dispose);
        });
    }

    public Key KeyBinding
    {
        get => (Key)this.GetValue(KeyBindingProperty);
        set => this.SetValue(KeyBindingProperty, value);
    }
}