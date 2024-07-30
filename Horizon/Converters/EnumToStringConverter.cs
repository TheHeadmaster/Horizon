using System.Globalization;
using System.Windows.Data;

namespace Horizon.Converters;

/// <summary>
/// Converts an <see cref="enum" /> to a <see cref="string" /> and back.
/// </summary>
public sealed class EnumToStringConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            string EnumString = Enum.GetName(value.GetType(), value) ?? string.Empty;
            return EnumString;
        }
        catch
        {
            return string.Empty;
        }
    }

    // No need to implement converting back on a one-way binding
    /// <inheritdoc />
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
}