using Horizon.Core.Data.Json;
using Horizon.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    public class ProjectFile : JFile, IFileToModel<Project>
    {
        public string Name { get; set; }

        public Project CreateModel()
        {
            Project project = new Project()
            {
                Name = this.Name,
                FileName = this.FileName,
                FilePath = this.FilePath
            };
            return project;
        }

        public void PopulateFile(Project model)
        {
            this.Name = model.Name;
            this.FileName = model.FileName;
            this.FilePath = model.FilePath;
        }
    }
}