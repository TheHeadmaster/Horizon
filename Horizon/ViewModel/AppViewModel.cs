using Horizon.API;
using Horizon.ObjectModel;
using Nito.Disposables.Internals;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;

namespace Horizon.ViewModel;

public sealed class AppViewModel : ReactiveObject
{
    public AppViewModel()
    {
        this.WhenAnyValue(x => x.Plugins)
            .Subscribe(plugins =>
            {
                var templates = plugins.Select(x => Assembly.GetAssembly(x.GetType()))
                    .WhereNotNull()
                    .SelectMany(x => x.GetTypes().Where(y => y.IsSubclassOf(typeof(ProjectFile))))
                    .Select(x => (ProjectFile?)Activator.CreateInstance(x))
                    .WhereNotNull();
                this.AvailableTemplates = new ObservableCollection<ProjectFile>(templates);
            });
    }

    [Reactive]
    public ObservableCollection<IPlugin> Plugins { get; set; } = [];

    [Reactive]
    public ObservableCollection<ProjectFile> AvailableTemplates { get; set; }

    [Reactive]
    public ProjectFile? CurrentProject { get; set; }
}