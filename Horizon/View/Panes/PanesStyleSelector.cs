using Horizon.ViewModel.Panes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Horizon.View.Panes;

public sealed class PanesStyleSelector : StyleSelector
{
    public Style? ToolStyle { get; set; }
    public Style? PageStyle { get; set; }

    public override Style SelectStyle(object item, DependencyObject container) => item switch
    {
        ToolViewModel => this.ToolStyle ?? base.SelectStyle(item, container),
        PageViewModel => this.PageStyle ?? base.SelectStyle(item, container),
        _ => base.SelectStyle(item, container)
    };
}