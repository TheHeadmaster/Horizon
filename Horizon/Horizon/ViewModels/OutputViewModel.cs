using Horizon.Controls;
using Horizon.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Horizon.ViewModels
{
    /// <summary>
    /// The ViewModel for the Output control.
    /// </summary>
    public class OutputViewModel : ReactiveObject
    {
        [Reactive]
        public string OutputText { get; set; }

        public Output View { get; }

        public OutputViewModel(Output view)
        {
            this.View = view;
        }
    }
}