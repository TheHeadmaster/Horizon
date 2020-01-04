using Horizon.Core.Data.Json;
using Horizon.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    public class MetadataFile : JFile, IFileToModel<Mod>
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string ID { get; set; }

        [JsonProperty("friendlyName")]
        public string Name { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        public Mod CreateModel()
        {
            Mod mod = new Mod
            {
                Author = this.Author,
                ID = this.ID,
                Name = this.Name,
                Priority = this.Priority,
                Version = this.Version,
                Description = this.Description,
                Items = new List<Item>(),
                CodeBank = new List<Code>()
            };

            foreach (string file in Directory.GetFiles(this.FilePath, "*.item", SearchOption.AllDirectories))
            {
                ItemFile itemFile = Load<ItemFile>(file);
                mod.Items.Add(itemFile.CreateModel());
            }

            foreach (string file in Directory.GetFiles(this.FilePath, "*.lua", SearchOption.AllDirectories))
            {
                Code code = new Code(file);
                code.Load();
                mod.CodeBank.Add(code);
            }
            return mod;
        }

        public void PopulateFile(Mod model)
        {
            this.ID = model.ID;
            this.Author = model.Author;
            this.Name = model.Name;
            this.Priority = model.Priority;
            this.Version = model.Version;
            this.Description = model.Description;
        }
    }
}