using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModel;

public sealed class LayoutInitializer : ILayoutUpdateStrategy
{
    public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
    {
        // AvalonDock wants to add the anchorable into destinationContainer just for test provide a new anchorable pane if the
        // pane is floating let the manager go ahead
        LayoutAnchorablePane? destPane = destinationContainer as LayoutAnchorablePane;
        if (destinationContainer?.FindParent<LayoutFloatingWindow>() is not null)
        {
            return false;
        }

        LayoutAnchorablePane? toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == "ToolsPane");
        if (toolsPane is not null)
        {
            toolsPane.Children.Add(anchorableToShow);
            return true;
        }

        return false;
    }

    public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
    { }

    public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer) => false;

    public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
    { }
}