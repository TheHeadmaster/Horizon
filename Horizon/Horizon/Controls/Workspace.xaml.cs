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
    /// Handles the controls and AvalonDock for the IDE window.
    /// </summary>
    public partial class Workspace : ReactiveUserControl<WorkspaceViewModel>
    {
        public static Workspace Instance { get; private set; }

        public Workspace()
        {
            this.InitializeComponent();
            Instance = this;
            this.ViewModel = new WorkspaceViewModel();

            this.WhenActivated(dispose =>
            {
                this.OneWayBind(this.ViewModel,
                    vm => vm.Documents,
                    view => view.MainDock.DocumentsSource)
                .DisposeWith(dispose);
            });
        }
    }
}