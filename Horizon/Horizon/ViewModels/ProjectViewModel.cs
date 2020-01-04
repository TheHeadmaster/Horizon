using Horizon.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class ProjectViewModel : DocumentControlViewModel<Project>
    {
        public override Project Model { get; set; }
    }
}