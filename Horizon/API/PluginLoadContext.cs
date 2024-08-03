using System.Reflection;
using System.Runtime.Loader;

namespace Horizon.API;

public class PluginLoadContext(string pluginPath) : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver resolver = new(pluginPath);

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        string? assemblyPath = this.resolver.ResolveAssemblyToPath(assemblyName);
        return assemblyPath is not null ? this.LoadFromAssemblyPath(assemblyPath) : null;
    }

    protected override nint LoadUnmanagedDll(string unmanagedDllName)
    {
        string? libraryPath = this.resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        return libraryPath is not null ? this.LoadUnmanagedDllFromPath(libraryPath) : IntPtr.Zero;
    }
}