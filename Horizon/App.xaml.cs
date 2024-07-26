using ReactiveUI;
using Serilog;
using Serilog.Formatting.Compact;
using Splat;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Horizon;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static bool IsDebug { get; private set; }

    public static string Version { get; private set; } = Assembly.GetExecutingAssembly()
        ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
        ?.InformationalVersion ?? "unknown-alpha";

    protected override async void OnStartup(StartupEventArgs args)
    {
        this.SetDebugFlag();
        this.InitializeLogging();
        base.OnStartup(args);

        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
    }

    private void InitializeApplication()
    {
    }

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
    }

    private void SetDebugFlag()
    {
#if DEBUG
        IsDebug = true;
#endif
    }
}