using Horizon.ObjectModel;
using Horizon.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    public class UserMetaFile : JFile, IFileToModel<UserMeta>
    {
        public bool OpenLastProjectOnStartup { get; set; }

        public List<RecentItem> RecentlyOpenedProjects { get; set; } = new List<RecentItem>();

        public UserMeta CreateModel()
        {
            UserMeta userMeta = new UserMeta
            {
                OpenLastProjectOnStartup = this.OpenLastProjectOnStartup,
                RecentlyOpenedProjects = new ObservableCollection<RecentItem>(this.RecentlyOpenedProjects),
                FilePath = this.FilePath,
                FileName = this.FileName
            };
            return userMeta;
        }

        public void PopulateFile(UserMeta model)
        {
            this.OpenLastProjectOnStartup = model.OpenLastProjectOnStartup;
            this.RecentlyOpenedProjects = model.RecentlyOpenedProjects.ToList();
            this.FilePath = model.FilePath;
            this.FileName = model.FileName;
        }
    }
}