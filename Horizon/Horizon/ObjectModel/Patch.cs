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
    public class Patch : DocumentControl
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public override string Header => "";

        public List<PatchOperation> PatchOperations { get; set; }

        public override DocumentControlViewModel CreateViewModel() => null;

        public override List<Error> EvaluateErrors() => new List<Error>();

        public void Save() { }
    }
}