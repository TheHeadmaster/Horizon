using Horizon.View.Windows;
using ReactiveUI;
using Serilog;
using Serilog.Formatting.Compact;
using Splat;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace Horizon;

/// <summary>
/// Handles application startup and metadata.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Gets whether this application is running in debug mode or not.
    /// </summary>
    public static bool IsDebug { get; private set; }

    private static string? userDirectory;

    /// <summary>
    /// Gets the User directory for this application.
    /// </summary>
    public static string UserDirectory
    {
        get
        {
            if (userDirectory is null)
            {
                userDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Horizon");

                if (!Directory.Exists(userDirectory))
                {
                    Directory.CreateDirectory(userDirectory);
                }
            }

            return userDirectory;
        }
    }

    /// <summary>
    /// Gets the version of the application as a string.
    /// </summary>
    public static string Version { get; private set; } = Assembly.GetExecutingAssembly()
        ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
        ?.InformationalVersion ?? "unknown-alpha";

    /// <summary>
    /// The workspace.
    /// </summary>
    private static Workspace? workspace;

    /// <summary>
    /// Gets all the startup tasks that run while the splash screen is displayed.
    /// </summary>
    /// <returns></returns>
    private Queue<(string label, Action action)> GetStartupTasks()
    {
        Queue<(string label, Action action)> tasks = new();

        tasks.Enqueue(("Initializing...", this.InitializeApplication));

        tasks.Enqueue(("Opening...", () => Current.Dispatcher.Invoke(() =>
        {
            workspace = new();
            Current.MainWindow = workspace;
        })));

        return tasks;
    }

    /// <inheritdoc />
    protected override async void OnStartup(StartupEventArgs args)
    {
        SetDebugFlag();
        this.InitializeLogging();
        base.OnStartup(args);

        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

        LoadingSplash splash = new();

        splash.Show();

        if (splash.ViewModel is not null)
        {
            await splash.ViewModel.StartRunningInBackground(this.GetStartupTasks());
        }

        splash.Close();

        Log.Information("Initialization complete.");

        workspace!.Show();
    }

    /// <summary>
    /// Initializes the application in the background.
    /// </summary>
    private void InitializeApplication()
    {
        // TODO: Place any other startup tasks here, such as loading the most recent project, checking for updates, etc.
    }

    /// <summary>
    /// Initializes logging and emits welcome messages.
    /// </summary>
    private void InitializeLogging()
    {
        LoggerConfiguration config = new();

        if (IsDebug)
        {
            config.MinimumLevel.Debug();
        }
        else
        {
            config.MinimumLevel.Information();
        }

        if (!Directory.Exists("Logs"))
        {
            Directory.CreateDirectory("Logs");
        }

        DateTime now = DateTime.UtcNow;
        string fileName = $"Session @ UTC {now.Year}-{now.Month:D2}-{now.Day:D2} {now.Hour:D2}-{now.Minute:D2}-{now.Second:D2}";

        config.WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(new CompactJsonFormatter(), $"Logs/{fileName}.log");

        Log.Logger = config.CreateLogger();

        Log.Information("Welcome to Horizon Version {Version}", Version);

        if (IsDebug)
        {
            Log.Warning("This assembly is running in DEBUG mode.");
        }

        AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
        this.DispatcherUnhandledException += this.OnUnhandledException;
    }

    /// <summary>
    /// Fires when the <see cref="AppDomain" /> throws an exception that wasn't handled.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        Log.Fatal((Exception)args.ExceptionObject, "Horizon encountered a critical error!");
    }

    /// <summary>
    /// Fires when the WPF <see cref="Dispatcher" /> throws an exception that wasn't handled.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event arguments.</param>
    private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
    {
        Log.Fatal(args.Exception, "Horizon encountered a critical error!");
    }

    /// <summary>
    /// Sets the debug flag.
    /// </summary>
    private static void SetDebugFlag()
    {
#if DEBUG
        IsDebug = true;
#endif
    }

    /// <inheritdoc />
    protected override void OnExit(ExitEventArgs args)
    {
        Log.Information("Closing...");

        base.OnExit(args);

        Log.CloseAndFlush();
    }
}