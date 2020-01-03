using Horizon.UI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    /// <summary>
    /// The view model for the Launcher window.
    /// </summary>
    public class LauncherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string LauncherPath { get; set; } = @"C:\Archives\Development\Erisa's Starbound";

        public ObservableCollection<ModPresentation> Mods { get; set; } = new ObservableCollection<ModPresentation>();

        public string Version { get; set; }

        public LauncherViewModel()
        {
            string text = File.ReadAllText(Path.Combine(this.LauncherPath, "storage", "starbound.log"));
            Regex reg = new Regex(@"Client Version ([0-9][\.][0-9][\.][0-9])", RegexOptions.IgnoreCase);
            Match m = reg.Match(text);

            this.Version = m.Groups[1].Value;
            this.LoadMods();
        }

        private void LoadMods()
        {
            foreach (string path in Directory.GetFiles(Path.Combine(this.LauncherPath, "mods"), "*.pak", SearchOption.TopDirectoryOnly))
            {
                this.Mods.Add(new ModPresentation { Enabled = true, Name = Path.GetFileNameWithoutExtension(path), FilePath = Path.GetDirectoryName(path), FileName = Path.GetFileName(path) });
            }
            foreach (string path in Directory.GetDirectories(Path.Combine(this.LauncherPath, "mods")))
            {
                if (File.Exists(Path.Combine(path, ".metadata")))
                {
                    this.Mods.Add(new ModPresentation { Enabled = true, Name = Path.GetFileNameWithoutExtension(path), FilePath = Path.GetDirectoryName(path), FileName = new DirectoryInfo(path).Name });
                }
            }

            if (!Directory.Exists(Path.Combine(this.LauncherPath, "unloaded")))
            {
                Directory.CreateDirectory(Path.Combine(this.LauncherPath, "unloaded"));
            }

            foreach (string path in Directory.GetFiles(Path.Combine(this.LauncherPath, "unloaded"), "*.pak", SearchOption.TopDirectoryOnly))
            {
                this.Mods.Add(new ModPresentation { Enabled = false, Name = Path.GetFileNameWithoutExtension(path), FilePath = Path.GetDirectoryName(path), FileName = Path.GetFileName(path) });
            }
            foreach (string path in Directory.GetDirectories(Path.Combine(this.LauncherPath, "unloaded")))
            {
                if (File.Exists(Path.Combine(path, ".metadata")))
                {
                    this.Mods.Add(new ModPresentation { Enabled = false, Name = Path.GetFileNameWithoutExtension(path), FilePath = Path.GetDirectoryName(path), FileName = new DirectoryInfo(path).Name });
                }
            }
        }
    }
}