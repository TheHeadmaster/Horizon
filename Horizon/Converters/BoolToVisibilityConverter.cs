using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Horizon.Converters;

/// <summary>
/// Converts a <see cref="bool" /> to a <see cref="Visibility" /> and back.
/// </summary>
public sealed class BoolToVisibilityConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            Visibility boolVisibility = (bool)value ? Visibility.Visible : Visibility.Collapsed;
            return boolVisibility;
        }
        catch
        {
            return Visibility.Collapsed;
        }
    }

    // No need to implement converting back on a one-way binding
    /// <inheritdoc />
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
}