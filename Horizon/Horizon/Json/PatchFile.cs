using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    public class PatchFile
    {
        /// <summary>
        /// Ensures all PatchFile objects save and load with the same settings.
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
        /// The list of all patch operations in the file.
        /// </summary>
        public List<PatchOperation> PatchOperations { get; set; }

        /// <summary>
        /// Creates a new PatchFile object.
        /// </summary>
        public PatchFile()
        {
        }

        /// <summary>
        /// Loads a PatchFile from a JSON file on disk. If one does not exist, returns a new
        /// PatchFile object.
        /// </summary>
        /// <typeparam name="T">
        /// The derived type of the PatchFile to return. Must derive from PatchFile.
        /// </typeparam>
        /// <param name="filePath">
        /// The path of the file on disk.
        /// </param>
        /// <param name="fileName">
        /// The name of the file on disk, including the extension.
        /// </param>
        /// <returns>
        /// </returns>
        public static PatchFile Load(string filePath, string fileName)
        {
            string path = Path.Combine(filePath, fileName);
            if (!File.Exists(path))
            {
                return new PatchFile { FilePath = filePath, FileName = fileName };
            }
            else
            {
                PatchFile patchFile = new PatchFile();
                patchFile.PatchOperations = JsonConvert.DeserializeObject<List<PatchOperation>>(File.ReadAllText(path));

                patchFile.FileName = fileName;
                patchFile.FilePath = filePath;
                return patchFile;
            }
        }

        /// <summary>
        /// Loads a PatchFile from a JSON file on disk. If one does not exist, returns a new
        /// PatchFile object.
        /// </summary>
        /// <typeparam name="T">
        /// The derived type of the PatchFile to return. Must derive from PatchFile.
        /// </typeparam>
        /// <param name="fullPath">
        /// The full path of the file on disk, including the name and extension.
        /// </param>
        /// <returns>
        /// </returns>
        public static PatchFile Load(string fullPath)
        {
            string filePath = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);
            return Load(filePath, fileName);
        }

        /// <summary>
        /// Saves the PatchFile to disk. If the file already exists, it is overwritten.
        /// </summary>
        public void Save()
        {
            string serialized = JsonConvert.SerializeObject(this.PatchOperations, JsonSettings);
            if (!Directory.Exists(this.FilePath))
            {
                Directory.CreateDirectory(this.FilePath);
            }
            File.WriteAllText(this.FullPath, serialized);
        }
    }
}