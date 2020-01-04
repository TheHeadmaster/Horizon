using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class WorkspaceViewModel
    {
        public ObservableCollection<DocumentControlViewModel> Documents => App.InterfaceData.Documents;
    }
}