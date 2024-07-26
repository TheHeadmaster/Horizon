using ReactiveUI;
using Splat;
using System.Reflection;
using System.Windows;

namespace Horizon;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs args)
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
    }
}