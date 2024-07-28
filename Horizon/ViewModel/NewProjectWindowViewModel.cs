using Horizon.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.IO;

namespace Horizon.ViewModel;

public sealed class NewProjectWindowViewModel : ReactiveObject
{
    public NewProjectWindowViewModel()
    {
    }

    [Reactive]
    public ProjectFile Project { get; set; } = new() { Name = "Project Name", FilePath = Path.Combine(App.UserDirectory, "Project.horizon") };
}