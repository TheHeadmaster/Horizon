using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Horizon.Converters;

/// <summary>
/// Converts a <see cref="string" /> path to an <see cref="BitmapImage" /> Source and back.
/// </summary>
public class StringToImageSourceConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? path = value as string;
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        BitmapImage src = new();

        try
        {
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
        }
        catch (UriFormatException)
        {
            return null;
        }

        return src;
    }

    // No need to implement converting back on a one-way binding
    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
}