using Horizon.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class IDEWindowViewModel
    {
        public Project CurrentProject { get; private set; }

        public void LoadUserMeta()
        {
            UserMetaFile metadataFile = JFile.Load<UserMetaFile>(App.AssemblyDirectory, "metadata.json");
            App.Metadata = metadataFile.CreateModel();
            App.Metadata.Save();

            if (App.Metadata.OpenLastProjectOnStartup && !(App.Metadata.LastOpenedProject is null))
            {
                ProjectFile projectFile = JFile.Load<ProjectFile>(App.Metadata.LastOpenedProject.Path, "Project.json");
                App.CurrentProject = projectFile.CreateModel();
                App.CurrentProject.LoadMods();
            }
        }
    }
}