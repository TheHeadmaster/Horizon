using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModel.Panes;

public abstract class PageViewModel : PaneViewModel
{
    public PageViewModel()
    {
        this.WhenAnyValue(x => x.ID)
            .Select(id => $"{this.GetType().Name}|{id}")
            .ToPropertyEx(this, x => x.ContentID);
    }

    [Reactive]
    public int ID { get; set; }

    [Reactive]
    public bool IsDirty { get; set; }

    [Reactive]
    public bool IsReadOnly { get; set; }

    [Reactive]
    public string? ReadOnlyReason { get; set; }
}