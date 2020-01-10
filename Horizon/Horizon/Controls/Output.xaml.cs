using Horizon.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Horizon.Controls
{
    /// <summary>
    /// Outputs information to a control in the IDE.
    /// </summary>
    public partial class Output : ReactiveUserControl<OutputViewModel>
    {
        public static Output Instance { get; private set; }

        public Output()
        {
            this.InitializeComponent();
            Instance = this;

            this.ViewModel = new OutputViewModel(this);

            this.WhenActivated(dispose =>
            {
                this.OneWayBind(this.ViewModel,
                    vm => vm.OutputText,
                    view => view.OutputTextBox.Text)
                .DisposeWith(dispose);
            });
        }
    }
}