using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Media;

namespace Horizon.ViewModel.Panes;

public abstract class PaneViewModel : ReactiveObject
{
    public abstract string Title { get; set; }

    [Reactive]
    public ImageSource? IconSource { get; set; }

    [ObservableAsProperty]
    public string ContentID { get; } = string.Empty;

    [Reactive]
    public bool IsSelected { get; set; }

    [Reactive]
    public bool IsActive { get; set; }
}