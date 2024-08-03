using DynamicData;
using Horizon.API;
using Horizon.ObjectModel;
using System.IO;

namespace Horizon.IO;

/// <summary>
/// Watches the assets directory for <see cref="AssetFile" /> file updates.
/// </summary>
public sealed class AssetsDirectoryMonitor(ProjectFile project, SourceCache<AssetFile, string> assets)
    : DirectoryMonitor(project.AssetsDirectory, "*.hasset")
{
    /// <inheritdoc />
    protected override Task FileDeleted(string fileName, string path, FileSystemEventArgs args)
    {
        // Check if the asset already exists
        AssetFile? asset = assets.Lookup(args.Name ?? string.Empty).Value;

        // Remove it if it did exist
        if (asset is not null)
        {
            assets.Remove(asset);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    protected override async Task ExtensionChanged(string? oldExtension, string? newExtension, RenamedEventArgs args)
    {
        // Check if the asset already exists
        AssetFile? asset = assets.Lookup(args.Name ?? string.Empty).Value;

        // If the new extension is not a hasset file then we should remove it if it exists
        if (newExtension is not "hasset" && asset is not null)
        {
            assets.Remove(asset);
        }
        else if (newExtension is "hasset" && asset is null)
        {
            // If the new extension is a hasset file then load it from disk and add it to the collection
            AssetFile? newAsset = await JsonFile.FromFile<AssetFile>(args.FullPath);
            if (newAsset is not null)
            {
                assets.AddOrUpdate(newAsset);
            }
        }
    }

    /// <inheritdoc />
    protected override async Task FileCreated(string fileName, string fullPath, FileSystemEventArgs args)
    {
        // Check if the asset already exists
        AssetFile? asset = assets.Lookup(args.Name ?? string.Empty).Value;

        if (asset is not null)
        {
            return;
        }

        // If it doesn't, load it from disk and add it to the collection
        AssetFile? newAsset = await JsonFile.FromFile<AssetFile>(args.FullPath);
        if (newAsset is not null)
        {
            assets.AddOrUpdate(newAsset);
        }
    }

    /// <inheritdoc />
    protected override Task FileModified(string fileName, string path, FileSystemEventArgs args) => Task.CompletedTask;

    /// <inheritdoc />
    protected override async Task FileNameChanged(string? oldName, string? newName, RenamedEventArgs args)
    {
        // Check if the asset already exists
        AssetFile? asset = assets.Lookup(args.OldName!).Value;

        // If it doesn't, load it from disk and add it to the collection
        if (asset is null)
        {
            AssetFile? newAsset = await JsonFile.FromFile<AssetFile>(args.FullPath);
            if (newAsset is not null)
            {
                assets.AddOrUpdate(newAsset);
            }
        }
        else
        {
            // If it does, change its path to update its values
            asset.FilePath = args.FullPath;
        }
    }

    /// <inheritdoc />
    protected override async Task DirectorySyncTick(IEnumerable<string> filePaths, IEnumerable<string> fileNames)
    {
        int currentPath = 0;

        // Look for files that haven't been loaded in yet
        foreach (string fileName in fileNames)
        {
            // Check if the asset already exists
            AssetFile? asset = assets.Lookup(fileName).Value;

            // If it doesn't, load it from disk and add it to the collection
            if (asset is null)
            {
                AssetFile? newAsset = await JsonFile.FromFile<AssetFile>(filePaths.ElementAt(currentPath));
                if (newAsset is not null)
                {
                    assets.AddOrUpdate(newAsset);
                }
            }

            currentPath++;
        }

        List<AssetFile> filesToRemove = [];

        // Look for loaded files that have been blown away
        foreach (AssetFile asset in assets.Items)
        {
            // If it is not in the directory, we can assume it was blown away or moved somewhere we don't care about
            if (!fileNames.Contains(asset.FileName))
            {
                // Put it on the chopping block
                filesToRemove.Add(asset);
            }
        }

        // Blow away all the desynced items
        foreach (AssetFile asset in filesToRemove)
        {
            assets.Remove(asset);
        }
    }
}