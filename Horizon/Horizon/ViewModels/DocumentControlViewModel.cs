using Horizon.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    /// <summary>
    /// Represents a ViewModel for a document control.
    /// </summary>
    public abstract class DocumentControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract DocumentControl GetModel();
    }

    /// <summary>
    /// Represents a ViewModel for a document control.
    /// </summary>
    /// <typeparam name="T">
    /// The document control type that this derived ViewModel belongs to.
    /// </typeparam>
    public abstract class DocumentControlViewModel<T> : DocumentControlViewModel where T : DocumentControl
    {
        public abstract T Model { get; set; }

        public override DocumentControl GetModel() => this.Model;
    }
}