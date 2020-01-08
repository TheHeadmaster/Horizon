using Horizon.Json;
using Horizon.UI;
using Horizon.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Horizon.ObjectModel
{
    /// <summary>
    /// Houses user metadata, such as preferences.
    /// </summary>
    public class UserMeta : IModelToFile, INotifyPropertyChanged
    {
        /// <summary>
        /// Raises whenever a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<RecentItem> recentlyOpenedProjects = new ObservableCollection<RecentItem>();

        /// <summary>
        /// Gets or sets the name of the json file saved to disk.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the path of the json file saved to disk.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Returns the most recently opened project.
        /// </summary>
        public RecentItem LastOpenedProject => this.RecentlyOpenedProjects.LastOrDefault();

        /// <summary>
        /// Gets or sets whether to automatically open the most recently opened project when the
        /// program starts.
        /// </summary>
        public bool OpenLastProjectOnStartup { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of most recently opened projects. The program will keep a maximum
        /// of 10 of the most recent projects in memory, and remove older entries if it reaches this limit.
        /// </summary>
        public ObservableCollection<RecentItem> RecentlyOpenedProjects
        {
            get => this.recentlyOpenedProjects;

            set
            {
                this.recentlyOpenedProjects.CollectionChanged -= this.RecentlyOpenedProjects_CollectionChanged;
                this.recentlyOpenedProjects = value;
                this.recentlyOpenedProjects.CollectionChanged += this.RecentlyOpenedProjects_CollectionChanged;
            }
        }

        public UserMeta()
        {
            this.RecentlyOpenedProjects.CollectionChanged += this.RecentlyOpenedProjects_CollectionChanged;
        }

        private void RecentlyOpenedProjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.RecentlyOpenedProjects.GroupBy(x => new { x.Name, x.Path });
            List<RecentItem> uniques = this.RecentlyOpenedProjects.DistinctWhere(x => new { x.Name, x.Path }).ToList();
            this.RecentlyOpenedProjects = new ObservableCollection<RecentItem>(uniques);
            if (this.RecentlyOpenedProjects.Count > 10)
            {
                this.RecentlyOpenedProjects.Remove(this.RecentlyOpenedProjects.First());
            }
            this.OnPropertyChanged(nameof(this.RecentlyOpenedProjects));
        }

        protected void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Creates a new json data model, populates it with this UserMeta's data, and saves it to disk.
        /// </summary>
        public void Save()
        {
            UserMetaFile file = new UserMetaFile();
            file.PopulateFile(this);
            file.Save();
        }
    }
}