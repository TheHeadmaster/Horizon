using Horizon.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ObjectModel
{
    public class Project : DocumentControl, IModelToFile
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public override string Header => this.Name;

        public ObservableCollection<Mod> Mods { get; set; } = new ObservableCollection<Mod>();

        [Displayable(Category = StringConstants.Meta, Description = StringConstants.ProjectNameDescription, DisplayName = "Project Name", IsExpandable = false)]
        public string Name { get; set; }

        public void Close(bool saveBeforeClosing)
        {
            if (saveBeforeClosing)
            {
                ProjectFile file = new ProjectFile();
                file.FileName = "Project.json";
                file.FilePath = this.FilePath;
                file.PopulateFile(this);
                file.Save();
            }
            App.CurrentProject = null;
        }

        public override DocumentControlViewModel CreateViewModel() => new ProjectViewModel { Model = this };

        public override List<Error> EvaluateErrors() => new List<Error>();

        public void LoadMods()
        {
            foreach (string x in Directory.GetDirectories(Path.Combine(this.FilePath, "mods")))
            {
                MetadataFile metadataFile = JFile.Load<MetadataFile>(x, ".metadata");
                Mod mod = metadataFile.CreateModel();
                this.Mods.Add(mod);
            }
        }

        /// <summary>
        /// Creates a new json data model, populates it with this project's data, and saves it to disk.
        /// </summary>
        public void Save()
        {
            ProjectFile file = new ProjectFile();
            file.PopulateFile(this);
            file.Save();
        }
    }
}