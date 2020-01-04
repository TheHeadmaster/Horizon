using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    /// <summary>
    /// Allows implementation of conversion between json data models and object models. Place on the
    /// object model side.
    /// </summary>
    public interface IModelToFile
    {
        /// <summary>
        /// The filename that this object model saves to and loads from.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// The path of the file that this object model saves to and loads from.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Creates a new json data model, populates it with this object's data, and saves it to disk.
        /// </summary>
        void Save();
    }
}