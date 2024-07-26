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

    /// <summary>
    /// Gets the version of the application as a string.
    /// </summary>
    public static string Version { get; private set; } = Assembly.GetExecutingAssembly()
        ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
        ?.InformationalVersion ?? "unknown-alpha";

    /// <inheritdoc />
    protected override async void OnStartup(StartupEventArgs args)
    {
        this.SetDebugFlag();
        this.InitializeLogging();
        base.OnStartup(args);

        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
    }

    /// <summary>
    /// Initializes the application in the background.
    /// </summary>
    private void InitializeApplication()
    {
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
        string fileName = $"Session @ {now.Month}-{now.Day}-{now.Year} {now.Hour}-{now.Minute}-{now.Second}";

        config.WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(new CompactJsonFormatter(), $"Logs/{fileName}.json");

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
    private void SetDebugFlag()
    {
#if DEBUG
        IsDebug = true;
#endif
    }

    /// <inheritdoc />
    protected override void OnExit(ExitEventArgs args)
    {
        base.OnExit(args);

        Log.CloseAndFlush();
    }
}