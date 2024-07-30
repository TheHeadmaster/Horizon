using Horizon.ViewModel.Panes;
using ReactiveUI;

namespace Horizon.View.Panes;

/// <summary>
/// Interaction logic for ProjectExplorerPane.xaml
/// </summary>
public partial class ProjectExplorerPane
{
    public ProjectExplorerPane()
    {
        this.InitializeComponent();

        this.ViewModel = new ProjectExplorerViewModel();

        this.WhenActivated(dispose =>
        {
        });
    }
}