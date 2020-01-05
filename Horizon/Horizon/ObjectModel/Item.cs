using Horizon.Json;
using Horizon.UI;
using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Horizon.ObjectModel
{
    public class Item : DocumentControl, IModelToFile
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public override string Header => this.Name;

        [Memento]
        public string ID { get; set; }

        public string Image => Path.Combine(this.FilePath, this.ImageName);

        [Memento]
        public string ImageName { get; set; }

        [Displayable(Category = StringConstants.General, Description = StringConstants.NameDescription, DisplayName = "Name", IsExpandable = false)]
        [Memento]
        public string Name { get; set; }

        [Displayable(Category = StringConstants.General, Description = StringConstants.PriceDescription, DisplayName = "Price", IsExpandable = false)]
        [Memento]
        public int Price { get; set; }

        public override DocumentControlViewModel CreateViewModel() => new ItemViewModel { Model = this };

        public override List<Error> EvaluateErrors() => new List<Error>();

        /// <summary>
        /// Creates a new json data model, populates it with this item's data, and saves it to disk.
        /// </summary>
        public void Save()
        {
            ItemFile itemFile = new ItemFile();
            itemFile.PopulateFile(this);
            itemFile.Save();
        }
    }
}