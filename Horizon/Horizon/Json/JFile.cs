using Horizon.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    /// <summary>
    /// Represents a JSON file that can be loaded from or saved to.
    /// </summary>
    public class JFile
    {
        /// <summary>
        /// Ensures all JFile objects save and load with the same settings.
        /// </summary>
        [JsonIgnore]
        private static JsonSerializerSettings JsonSettings { get; } = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// The name of the file on disk, including the extension.
        /// </summary>
        [JsonIgnore]
        public string FileName { get; set; }

        /// <summary>
        /// The directory of the file on disk.
        /// </summary>
        [JsonIgnore]
        public string FilePath { get; set; }

        /// <summary>
        /// The full name of the file on disk.
        /// </summary>
        [JsonIgnore]
        public string FullPath => Path.Combine(this.FilePath, this.FileName);

        /// <summary>
        /// Creates a new JFile object.
        /// </summary>
        public JFile()
        {
        }

        /// <summary>
        /// Loads a JFile from a JSON file on disk. If one does not exist, returns a new JFile object.
        /// </summary>
        /// <typeparam name="T">
        /// The derived type of the JFile to return. Must derive from JFile.
        /// </typeparam>
        /// <param name="filePath">
        /// The path of the file on disk.
        /// </param>
        /// <param name="fileName">
        /// The name of the file on disk, including the extension.
        /// </param>
        /// <returns>
        /// </returns>
        [JFileLoadLog]
        public static T Load<T>(string filePath, string fileName) where T : JFile, new()
        {
            string path = Path.Combine(filePath, fileName);
            if (!File.Exists(path))
            {
                return new T { FilePath = filePath, FileName = fileName };
            }
            else
            {
                T jFile = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                jFile.FileName = fileName;
                jFile.FilePath = filePath;
                return jFile;
            }
        }

        /// <summary>
        /// Loads a JFile from a JSON file on disk. If one does not exist, returns a new JFile object.
        /// </summary>
        /// <typeparam name="T">
        /// The derived type of the JFile to return. Must derive from JFile.
        /// </typeparam>
        /// <param name="fullPath">
        /// The full path of the file on disk, including the name and extension.
        /// </param>
        /// <returns>
        /// </returns>
        public static T Load<T>(string fullPath) where T : JFile, new()
        {
            string filePath = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);
            return Load<T>(filePath, fileName);
        }

        /// <summary>
        /// Saves the JFile to disk. If the file already exists, it is overwritten.
        /// </summary>
        public void Save()
        {
            string serialized = JsonConvert.SerializeObject(this, JsonSettings);
            if (!Directory.Exists(this.FilePath))
            {
                Directory.CreateDirectory(this.FilePath);
            }
            File.WriteAllText(this.FullPath, serialized);
        }
    }
}