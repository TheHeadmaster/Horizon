using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Horizon.ObjectModel;

public class ProjectTemplate : ReactiveObject
{
    [Reactive]
    public string Name { get; set; } = string.Empty;

    [Reactive]
    public string Description { get; set; } = string.Empty;

    [Reactive]
    public List<string> Tags { get; set; } = [];
}