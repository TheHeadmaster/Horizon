using DynamicData;
using Horizon.API;
using Horizon.IO;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Reactive.Linq;

namespace Horizon.ObjectModel;

/// <summary>
/// Contains project metadata and serves as a root for related project files in the directory it is contained in.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public abstract class ProjectFile : JsonFile
{
    private readonly SourceCache<AssetFile, string> assets = new(asset => asset.ID);

    private AssetsDirectoryMonitor assetsMonitor = null!;

    /// <inheritdoc />
    public ProjectFile() : base()
    {
        // The assets directory is based on the project directory
        this.WhenAnyValue(project => project.FileDirectory)
            .Select(path => string.IsNullOrWhiteSpace(path) ? string.Empty : path)
            .Select(directory => Path.Combine(directory, "Assets"))
            .ToPropertyEx(this, project => project.AssetsDirectory);
    }

    public abstract string TemplateName { get; }

    public abstract string TemplateDescription { get; }

    public abstract List<string> Tags { get; }

    /// <summary>
    /// The name of the <see cref="ProjectFile" />.
    /// </summary>
    [JsonProperty, Reactive]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The directory containing the project's <see cref="AssetFile" /> files.
    /// </summary>
    [ObservableAsProperty]
    public string AssetsDirectory { get; } = string.Empty;

    public virtual Task Setup() => Task.CompletedTask;

    /// <inheritdoc />
    public override async Task Load()
    {
        await this.GetInitialAssets();

        this.assetsMonitor = new(this, this.assets);
    }

    /// <inheritdoc />
    public override async Task Unload()
    {
        await this.assetsMonitor.Close();
    }

    /// <summary>
    /// Connects to the Assets collection.
    /// </summary>
    /// <returns>An observable change set.</returns>
    public IObservable<IChangeSet<AssetFile, string>> ConnectModules() => this.assets.Connect();

    /// <summary>
    /// Loads the <see cref="AssetFile" /> files that are initially on disk when the <see cref="ProjectFile" /> is loaded.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    private async Task GetInitialAssets()
    {
        if (!Directory.Exists(this.AssetsDirectory))
        {
            Directory.CreateDirectory(this.AssetsDirectory);
        }

        foreach (string assetFile in Directory.GetFiles(this.AssetsDirectory, "*.hasset"))
        {
            AssetFile? asset = await FromFile<AssetFile>(assetFile);

            if (asset is null)
            {
                continue;
            }

            this.assets.AddOrUpdate(asset);
        }
    }
}