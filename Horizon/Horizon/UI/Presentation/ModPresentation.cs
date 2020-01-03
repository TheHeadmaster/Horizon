using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Gets or sets whether the mod is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the name of the mod.
        /// </summary>
        public string Name { get; set; }
    }
}