using Horizon.Core.Data.Json;
using Horizon.Json;
using Horizon.UI;
using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Horizon.ObjectModel
{
    public class Mod : DocumentControl, IModelToFile
    {
        protected static List<Mod> Instances { get; } = new List<Mod>();

        public string Author { get; set; }

        public List<Code> CodeBank { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public override string Header => this.Name;

        public string ID { get; set; }

        public List<Item> Items { get; set; }

        [Displayable(Category = StringConstants.Meta, Description = StringConstants.ModNameDescription, DisplayName = "Name", IsExpandable = false)]
        public string Name { get; set; }

        public int Priority { get; set; }

        public string Version { get; set; }

        public Mod()
        {
            Instances.Add(this);
        }

        public override DocumentControlViewModel CreateViewModel() => new ModViewModel { Model = this };

        public override List<Error> EvaluateErrors()
        {
            List<Error> errors = new List<Error>();
            List<Mod> allexceptself = Instances.Where(x => x != this).ToList();
            List<Mod> duplicateIDs = allexceptself.Where(x => x.ID == this.ID).ToList();
            foreach (Mod duplicate in duplicateIDs)
            {
                Error newError = new Error(this);
                newError.Project = this.Name;
                newError.ErrorCode = "HE001";
                newError.Description = $"ID of Mod '{this.Name}' and '{duplicate.Name}' are the same ({this.ID}). Mod IDs must be unique.";
                newError.ErrorType = ErrorType.Fatal;
                errors.Add(newError);
            }
            return errors;
        }

        /// <summary>
        /// Creates a new json data model, populates it with this Mod's data, and saves it to disk.
        /// </summary>
        public void Save()
        {
            MetadataFile file = new MetadataFile();
            file.PopulateFile(this);
            file.Save();
        }
    }
}