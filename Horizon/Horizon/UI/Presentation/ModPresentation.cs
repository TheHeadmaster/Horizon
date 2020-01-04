using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.UI.Presentation
{
    /// <summary>
    /// Representation of Mods for the ModListBox.
    /// </summary>
    public class ModPresentation
    {
        private bool enabled;

        /// <summary>
        /// Gets or sets whether the mod is enabled or not.
        /// </summary>
        public bool Enabled
        {
            get => this.enabled;

            set
            {
                this.enabled = value;
                if (this.FullPath is null) { return; }
                if (this.enabled)
                {
                    Directory.Move(this.FullPath, Path.Combine(App.LauncherMeta.LauncherPath, "mods", this.FileName));
                    this.FilePath = Path.Combine(App.LauncherMeta.LauncherPath, "mods");
                }
                else
                {
                    Directory.Move(this.FullPath, Path.Combine(App.LauncherMeta.LauncherPath, "unloaded", this.FileName));
                    this.FilePath = Path.Combine(App.LauncherMeta.LauncherPath, "unloaded");
                }
            }
        }

        /// <summary>
        /// Gets or sets the filename of the mod.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the filepath of the mod.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the path of the mod.
        /// </summary>
        public string FullPath
        {
            get
            {
                if (this.FilePath is null || this.FileName is null) { return null; }
                return Path.Combine(this.FilePath, this.FileName);
            }
        }

        /// <summary>
        /// Gets or sets the name of the mod.
        /// </summary>
        public string Name { get; set; }
    }
}