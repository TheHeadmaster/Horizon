using Horizon.Json;
using Horizon.UI;
using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ObjectModel
{
    public class RenderOptions : DocumentControl, IModelToFile
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public override string Header => "RenderOptions";

        public override DocumentControlViewModel CreateViewModel() => new RenderOptionsViewModel { Model = this };

        public override List<Error> EvaluateErrors() => new List<Error>();

        /// <summary>
        /// Creates a new json data model, populates it with this item's data, and saves it to disk.
        /// </summary>
        public void Save()
        {
            //ItemFile itemFile = new ItemFile();
            //itemFile.PopulateFile(this);
            //itemFile.Save();
        }
    }
}