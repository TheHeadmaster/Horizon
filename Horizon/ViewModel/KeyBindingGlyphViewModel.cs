using Horizon.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Horizon.ViewModel;

public sealed class KeyBindingGlyphViewModel : ReactiveObject
{
    public KeyBindingGlyphViewModel()
    {
        this.WhenAnyValue(x => x.Height, x => x.WidthMultiplier, (height, widthMultiplier) => height * widthMultiplier)
            .Subscribe(x => this.Width = x);

        this.WhenAnyValue(x => x.GlyphImage)
            .Select(key => this.GlyphImage == this.KeyGlyph || this.GlyphImage == this.KeyGlyphWindows
                           ? 1.0
                           : this.GlyphImage == this.KeyGlyphMedium
                           ? 2.0
                           : 3.0)
            .Subscribe(x => this.WidthMultiplier = x);

        this.WhenAnyValue(x => x.GlyphKey)
            .Select(TranslateKeyToText)
            .Subscribe(x => this.GlyphText = x);

        this.WhenAnyValue(x => x.GlyphKey)
            .Select(key => key == Key.LWin
                           ? this.KeyGlyphWindows
                           : key == Key.Space
                           ? this.KeyGlyphSpace
                           : IsModifierKey(key)
                           ? this.KeyGlyphMedium
                           : this.KeyGlyph)
            .Subscribe(x => this.GlyphImage = x);

        string path = "pack://application:,,,/Resources/Images/Key.png";

        BitmapImage src = new();

        try
        {
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
        }
        catch (UriFormatException)
        {
        }

        this.KeyGlyph = src;

        src = new();
        path = "pack://application:,,,/Resources/Images/Key Medium.png";
        try
        {
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
        }
        catch (UriFormatException)
        {
        }

        this.KeyGlyphMedium = src;

        src = new();
        path = "pack://application:,,,/Resources/Images/Key Space.png";
        try
        {
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
        }
        catch (UriFormatException)
        {
        }

        this.KeyGlyphSpace = src;

        src = new();
        path = "pack://application:,,,/Resources/Images/Key Windows.png";
        try
        {
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
        }
        catch (UriFormatException)
        {
        }

        this.KeyGlyphWindows = src;
    }

    [Reactive]
    public string GlyphText { get; set; } = string.Empty;

    [Reactive]
    public Key GlyphKey { get; set; }

    [Reactive]
    public double Height { get; set; } = 24;

    [Reactive]
    public double WidthMultiplier { get; set; } = 1;

    [Reactive]
    public double Width { get; set; } = 24;

    [Reactive]
    public ImageSource? GlyphImage { get; set; }

    private ImageSource KeyGlyph { get; set; }

    private ImageSource KeyGlyphMedium { get; set; }

    private ImageSource KeyGlyphSpace { get; set; }

    private ImageSource KeyGlyphWindows { get; set; }

    private static bool IsModifierKey(Key key) => KeyConstants.MediumSizeKeys.Contains(key);

    private static string TranslateKeyToText(Key key)
        => KeyConstants.KeyNames.TryGetValue(key, out string? name) ? name : Enum.GetName(key) ?? string.Empty;
}