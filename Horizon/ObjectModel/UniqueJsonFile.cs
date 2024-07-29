using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;

namespace Horizon.ObjectModel;

/// <summary>
/// Represents a <see cref="JsonFile" /> that has a unique ID based on its file name.
/// </summary>
public abstract class UniqueJsonFile : JsonFile
{
    /// <inheritdoc />
    public UniqueJsonFile() : base()
    {
        // The ID is based on the file name
        this.WhenAnyValue(file => file.FileName)
            .Select(name => name)
            .ToPropertyEx(this, file => file.ID);
    }

    /// <summary>
    /// The unique ID of the <see cref="UniqueJsonFile" />. Based on its file name.
    /// </summary>
    [ObservableAsProperty]
    public string ID { get; } = string.Empty;

    /// <summary>
    /// The friendly name of the <see cref="UniqueJsonFile" />.
    /// </summary>
    [JsonProperty, Reactive]
    public string? DisplayName { get; set; }
}