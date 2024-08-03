using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace Horizon.API;

internal static class PluginLoader
{
    internal static Task InitializePlugins()
    {
        List<string> pluginPaths = [];

        foreach (string folder in Directory.GetDirectories(App.PluginDirectory))
        {
            string? folderName = new DirectoryInfo(folder).Name;

            if (string.IsNullOrWhiteSpace(folderName))
            {
                continue;
            }

            if (!File.Exists(Path.Combine(folder, $"{folderName}.dll")))
            {
                continue;
            }

            pluginPaths.Add(Path.Combine(folder, $"{folderName}.dll"));
        }

        List<IPlugin> plugins = pluginPaths
            .SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreatePlugins(pluginAssembly);
            }).ToList();

        App.ViewModel.Plugins = new ObservableCollection<IPlugin>(plugins);

        return Task.CompletedTask;
    }

    internal static Assembly LoadPlugin(string pluginPath)
    {
        PluginLoadContext loadContext = new(pluginPath);
        return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
    }

    internal static IEnumerable<IPlugin> CreatePlugins(Assembly assembly)
    {
        int count = 0;

        foreach (Type type in assembly.GetTypes())
        {
            if (typeof(IPlugin).IsAssignableFrom(type))
            {
                IPlugin? result = Activator.CreateInstance(type) as IPlugin;
                if (result is not null)
                {
                    count++;
                    yield return result;
                }
            }
        }

        if (count == 0)
        {
            string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements IPlugin in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
    }
}