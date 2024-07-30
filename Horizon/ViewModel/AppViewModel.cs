using Horizon.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace Horizon.ViewModel;

public sealed class AppViewModel : ReactiveObject
{
    [Reactive]
    public ObservableCollection<ProjectTemplate> AvailableTemplates { get; set; } = [new() { Name = "Starbound Mod Project", Description = "A mod project for the game Starbound", Tags = ["Starbound", "Mod"] }];

    [Reactive]
    public ProjectFile? CurrentProject { get; set; }
}