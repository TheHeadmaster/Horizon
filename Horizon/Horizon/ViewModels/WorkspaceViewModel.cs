using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    /// <summary>
    /// The ViewModel for the workspace control.
    /// </summary>
    public class WorkspaceViewModel
    {
        public ObservableCollection<DocumentControlViewModel> Documents { get; set; } = new ObservableCollection<DocumentControlViewModel>();
    }
}