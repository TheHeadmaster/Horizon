using Horizon.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class ItemViewModel : DocumentControlViewModel<Item>
    {
        public override Item Model { get; set; }
    }
}