using Nito.AsyncEx.Synchronous;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Horizon.IO;

/// <summary>
/// Watches a directory for file changes and acts upon them.
/// </summary>
public abstract class DirectoryMonitor
{
    /// <summary>
    /// The underlying <see cref="FileSystemWatcher" />.
    /// </summary>
    private FileSystemWatcher? watcher;

    /// <summary>
    /// The timer used to perform periodic directory syncs.
    /// </summary>
    private Timer? directorySyncTimer;

    /// <summary>
    /// Creates a new instance of <see cref="DirectoryMonitor" />.
    /// </summary>
    /// <param name="directory">The directory to watch.</param>
    /// <param name="filter">The filter to use on files in the directory.</param>
    public DirectoryMonitor(string directory, string filter)
    {
        this.watcher = new FileSystemWatcher()
        {
            Path = directory,
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
            Filter = filter
        };

        this.watcher.Created += this.OnFileCreated;
        this.watcher.Deleted += this.OnFileDeleted;
        this.watcher.Changed += this.OnFileChanged;
        this.watcher.Renamed += this.OnFileRenamed;
        this.watcher.EnableRaisingEvents = true;

        this.directorySyncTimer = new Timer(TimeSpan.FromSeconds(10)) { AutoReset = true };
        this.directorySyncTimer.Elapsed += this.OnDirectorySyncTick;
    }

    /// <summary>
    /// Closes the <see cref="DirectoryMonitor" /> and breaks it down.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    public Task Close()
    {
        if (this.watcher is not null)
        {
            this.watcher.EnableRaisingEvents = false;
            this.watcher.Created -= this.OnFileCreated;
            this.watcher.Deleted -= this.OnFileDeleted;
            this.watcher.Changed -= this.OnFileChanged;
            this.watcher.Renamed -= this.OnFileRenamed;
            this.watcher.Dispose();
            this.watcher = null;
        }

        if (this.directorySyncTimer is not null)
        {
            this.directorySyncTimer.Stop();
            this.directorySyncTimer.Elapsed -= this.OnDirectorySyncTick;
            this.directorySyncTimer.Dispose();
            this.directorySyncTimer = null;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Fires when a directory sync occurs. Use this to pick up stragglers that aren't picked up by watcher events.
    /// </summary>
    /// <param name="filePaths">The paths of the files currently in the watched directory.</param>
    /// <param name="fileNames">The names of the files currently in the watched directory.</param>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    protected abstract Task DirectorySyncTick(IEnumerable<string> filePaths, IEnumerable<string> fileNames);

    /// <summary>
    /// Fires when a file has been deleted.
    /// </summary>
    /// <param name="fileName">The name of the file without extension.</param>
    /// <param name="path">The full path of the file.</param>
    /// <param name="args">The event arguments from the original delete event.</param>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    protected abstract Task FileDeleted(string fileName, string path, FileSystemEventArgs args);

    /// <summary>
    /// Fires when a file has been created.
    /// </summary>
    /// <param name="fileName">The name of the file without extension.</param>
    /// <param name="fullPath">The full path of the file.</param>
    /// <param name="args">The event arguments from the original create event.</param>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    protected abstract Task FileCreated(string fileName, string fullPath, FileSystemEventArgs args);

    /// <summary>
    /// Fires when a file's extension has changed.
    /// </summary>
    /// <param name="oldExtension">The old extension.</param>
    /// <param name="newExtension">The new extension.</param>
    /// <param name="args">The event arguments from the original rename event.</param>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    protected abstract Task ExtensionChanged(string? oldExtension, string? newExtension, RenamedEventArgs args);

    /// <summary>
    /// Fires when a file's name has changed.
    /// </summary>
    /// <param name="oldName">The old name.</param>
    /// <param name="newName">The new name.</param>
    /// <param name="args">The event arguments from the original rename event.</param>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    protected abstract Task FileNameChanged(string? oldName, string? newName, RenamedEventArgs args);

    /// <summary>
    /// Fires when a file has been modified.
    /// </summary>
    /// <param name="fileName">The name of the file without extension.</param>
    /// <param name="path">The full path of the file.</param>
    /// <param name="args">The event arguments from the original change event.</param>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    protected abstract Task FileModified(string fileName, string path, FileSystemEventArgs args);

    /// <summary>
    /// Handles a directory sync tick.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnDirectorySyncTick(object? sender, ElapsedEventArgs args)
    {
        if (this.watcher is not null)
        {
            string[] paths = Directory.GetFiles(this.watcher.Path, this.watcher.Filter);
            this.DirectorySyncTick(paths, paths.Select(path => Path.GetFileName(path))).WaitAndUnwrapException();
        }
    }

    /// <summary>
    /// Handles a file rename.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnFileRenamed(object sender, RenamedEventArgs args)
    {
        string? oldExtension = Path.GetExtension(args.OldName);
        string? newExtension = Path.GetExtension(args.Name);

        if (oldExtension != newExtension)
        {
            this.ExtensionChanged(oldExtension, newExtension, args).WaitAndUnwrapException();
        }

        string? oldFileName = Path.GetFileNameWithoutExtension(args.OldName);
        string? newFileName = Path.GetFileNameWithoutExtension(args.Name);

        if (oldFileName != newFileName)
        {
            this.FileNameChanged(oldFileName, newFileName, args).WaitAndUnwrapException();
        }
    }

    /// <summary>
    /// Handles a file change.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnFileChanged(object sender, FileSystemEventArgs args)
    {
        this.FileModified(Path.GetFileNameWithoutExtension(args.Name) ?? string.Empty, args.FullPath, args)
            .WaitAndUnwrapException();
    }

    /// <summary>
    /// Handles a file deletion.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnFileDeleted(object sender, FileSystemEventArgs args)
    {
        this.FileDeleted(Path.GetFileNameWithoutExtension(args.Name) ?? string.Empty, args.FullPath, args)
            .WaitAndUnwrapException();
    }

    /// <summary>
    /// Handles a file creation.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnFileCreated(object sender, FileSystemEventArgs args)
    {
        this.FileCreated(Path.GetFileNameWithoutExtension(args.Name) ?? string.Empty, args.FullPath, args)
            .WaitAndUnwrapException();
    }
}