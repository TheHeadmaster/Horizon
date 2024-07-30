using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;

namespace Horizon.ViewModel.Panes;

public abstract class ToolViewModel : PaneViewModel
{
    public ToolViewModel()
    {
        Observable.Return(this.GetType().Name)
            .ToPropertyEx(this, x => x.ContentID);
    }

    [Reactive]
    public bool IsVisible { get; set; } = true;
}