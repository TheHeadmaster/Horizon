using Horizon.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class CodeViewModel : DocumentControlViewModel<Code>
    {
        public override Code Model { get; set; }
    }
}