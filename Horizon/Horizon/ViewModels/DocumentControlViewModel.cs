using Horizon.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public abstract class DocumentControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract DocumentControl GetModel();
    }

    public abstract class DocumentControlViewModel<T> : DocumentControlViewModel where T : DocumentControl
    {
        public abstract T Model { get; set; }

        public override DocumentControl GetModel() => this.Model;
    }
}