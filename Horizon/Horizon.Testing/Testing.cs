using System;
using System.IO;
using System.Reflection;

namespace Horizon.Testing
{
    /// <summary>
    /// Collection of global testing constants or functions.
    /// </summary>
    public static class Testing
    {
        /// <summary>
        /// Gets the directory that the assembly is in.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}