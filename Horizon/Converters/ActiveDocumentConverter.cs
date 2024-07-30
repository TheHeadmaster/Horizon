using Horizon.ViewModel.Panes;
using ReactiveUI;

namespace Horizon.Converters;

/// <summary>
/// Converts an <see cref="object" /> to an active document binding.
/// </summary>
public sealed class ActiveDocumentConverter : IBindingTypeConverter
{
    /// <inheritdoc />
    public int GetAffinityForObjects(Type fromType, Type toType) => 0;

    /// <inheritdoc />
    public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
    {
        if (from is PageViewModel)
        {
            result = from;
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }
}