using Horizon.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class PreferencesViewModel : ReactiveObject
    {
        [Reactive]
        public bool OpenLastProjectOnStartup { get; set; }

        public PreferencesViewModel()
        {
            this.OpenLastProjectOnStartup = App.Metadata.OpenLastProjectOnStartup;
        }

        public void CommitPreferences()
        {
            App.Metadata.OpenLastProjectOnStartup = this.OpenLastProjectOnStartup;
            UserMetaFile metadataFile = new UserMetaFile();
            metadataFile.PopulateFile(App.Metadata);
            metadataFile.Save();
        }
    }
}