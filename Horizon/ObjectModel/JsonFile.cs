using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;
using System.IO;
using System.Reactive.Linq;

namespace Horizon.ObjectModel;

/// <summary>
/// Represents a <see cref="ReactiveObject" /> that has properties that can be saved to and loaded from a json file.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public abstract class JsonFile : ReactiveObject
{
    /// <inheritdoc />
    public JsonFile()
    {
        // The directory is based on the full file path
        this.WhenAnyValue(file => file.FilePath)
            .Select(path => string.IsNullOrWhiteSpace(path) ? string.Empty : path)
            .Select(Path.GetDirectoryName)
            .ToPropertyEx(this, file => file.FileDirectory);

        // The file name is based on the full file path
        this.WhenAnyValue(file => file.FilePath)
            .Select(path => string.IsNullOrWhiteSpace(path) ? string.Empty : path)
            .Select(path => Path.GetFileName(path))
            .ToPropertyEx(this, file => file.FileName);
    }

    /// <summary>
    /// The directory that the <see cref="JsonFile" /> is contained in.
    /// </summary>
    [ObservableAsProperty]
    public string FileDirectory { get; } = string.Empty;

    /// <summary>
    /// The path of the <see cref="JsonFile" />, including the filename and extension.
    /// </summary>
    [Reactive]
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// The name and extension of the <see cref="JsonFile" /> without the directory.
    /// </summary>
    [ObservableAsProperty]
    public string FileName { get; } = string.Empty;

    /// <summary>
    /// The version that the <see cref="JsonFile" /> was last saved as. Used to determine compatibility and migration.
    /// </summary>
    [JsonProperty, Reactive]
    public string Version { get; set; } = App.Version;

    /// <summary>
    /// Loads the <see cref="JsonFile" /> from disk.
    /// </summary>
    /// <typeparam name="TJsonFile">The type of the <see cref="JsonFile" />.</typeparam>
    /// <param name="path">The path of the <see cref="JsonFile" />, including the name and extension.</param>
    /// <returns>
    /// An awaitable <see cref="Task" /> that returns a <typeparamref name="TJsonFile" /> or <see langword="null" />.
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<TJsonFile?> FromFile<TJsonFile>(string path) where TJsonFile : JsonFile, new()
    {
        if (!File.Exists(path))
        {
            Log.Error("Attempted to load json file of type {JsonFileType} at path {JsonFilePath}, but it does not exist.", typeof(TJsonFile), path);
            return default;
        }

        string json = await File.ReadAllTextAsync(path);

        TJsonFile? file = JsonConvert.DeserializeObject<TJsonFile>(json)
            ?? throw new InvalidOperationException("The loaded file could not be deserialized.");

        file.FilePath = path;

        return file;
    }

    /// <summary>
    /// Loads the <see cref="JsonFile" /> from disk and casts it to the abstract type.
    /// </summary>
    /// <typeparam name="TJsonFile">The abstract base type of the <see cref="JsonFile" />.</typeparam>
    /// <param name="path">The path of the <see cref="JsonFile" />, including the name and extension.</param>
    /// <returns>
    /// An awaitable <see cref="Task" /> that returns a <typeparamref name="TJsonFile" /> or <see langword="null" />.
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<TJsonFile?> FromAbstractFile<TJsonFile>(string path) where TJsonFile : JsonFile
    {
        if (!File.Exists(path))
        {
            Log.Error("Attempted to load json file of type {JsonFileType} at path {JsonFilePath}, but it does not exist.", typeof(TJsonFile), path);
            return default;
        }

        string json = await File.ReadAllTextAsync(path);

        TJsonFile? file = JsonConvert.DeserializeObject(json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }) as TJsonFile
            ?? throw new InvalidOperationException("The loaded file could not be deserialized.");

        file.FilePath = path;

        return file;
    }

    /// <summary>
    /// Loads the <see cref="JsonFile" /> and performs initialization operations.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    public abstract Task Load();

    /// <summary>
    /// Unloads the <see cref="JsonFile" /> and performs cleanup operations.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    public abstract Task Unload();

    /// <summary>
    /// Saves the <see cref="JsonFile" /> to disk.
    /// </summary>
    public async Task Save()
    {
        Log.Debug("Saving file of type {JsonFileType} at path {JsonFilePath}.", this.GetType(), this.FilePath);
        string json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

        await File.WriteAllTextAsync(this.FilePath, json);
    }
}