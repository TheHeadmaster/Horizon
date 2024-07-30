using Horizon.ViewModel.Panes;
using System.Windows;
using System.Windows.Controls;

namespace Horizon.View.Panes;

public sealed class PanesTemplateSelector : DataTemplateSelector
{
    public DataTemplate? ProjectExplorerViewTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container) =>
        item switch
        {
            ProjectExplorerViewModel => this.ProjectExplorerViewTemplate,
            _ => base.SelectTemplate(item, container)
        };
}