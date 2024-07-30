using AvalonDock;
using AvalonDock.Layout.Serialization;
using Horizon.ViewModel.Panes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;

namespace Horizon.ViewModel;

public sealed class WorkspaceViewModel : ReactiveObject
{
    public WorkspaceViewModel()
    {
        this.SaveLayout = ReactiveCommand.Create<DockingManager?>(OnSaveLayout);
        this.LoadLayout = ReactiveCommand.Create<DockingManager?>(this.OnLoadLayout);
        this.DocumentClosing = ReactiveCommand.Create<DocumentClosingEventArgs>(this.OnDocumentClosing);

        App.ViewModel
            .WhenAnyValue(vm => vm.CurrentProject, vm => vm.CurrentProject!.Name, (x, y) => x)
            .Subscribe(x => this.Title = x is null ? "Horizon" : $"Horizon - {x.Name}");
    }

    [Reactive]
    public ObservableCollection<PageViewModel> Pages { get; set; } = [];

    [Reactive]
    public ObservableCollection<ToolViewModel> Tools { get; set; } = [new ProjectExplorerViewModel()];

    public ReactiveCommand<DockingManager?, Unit> SaveLayout { get; set; }

    public ReactiveCommand<DockingManager?, Unit> LoadLayout { get; set; }

    public ReactiveCommand<DocumentClosingEventArgs, Unit> DocumentClosing { get; set; }

    [Reactive]
    public PageViewModel? ActiveDocument { get; set; }

    [Reactive]
    public string Title { get; set; } = "Horizon";

    private static void OnSaveLayout(DockingManager? manager)
    {
        if (manager is null)
        {
            return;
        }

        using StringWriter fs = new();
        XmlLayoutSerializer xmlLayout = new(manager);

        xmlLayout.Serialize(fs);

        string xmlLayoutString = fs.ToString();

        if (string.IsNullOrWhiteSpace(xmlLayoutString))
        {
            return;
        }

        string fileName = Path.Combine(App.AppDataDirectory, "Layout.config");

        File.WriteAllText(fileName, xmlLayoutString);
    }

    private void OnDocumentClosing(DocumentClosingEventArgs args)
    {
        PageViewModel? page = this.Pages.FirstOrDefault(x => x.ContentID == args.Document.ContentId);

        if (page is null)
        {
            args.Cancel = true;
        }
        else
        {
            // prompt for save
            int x = 0;
        }
    }

    private void CreateDefaultLayout()
    {
        //this.Pages.Add(new EventPageViewModel());
    }

    private void ReloadContentOnStartup(LayoutSerializationCallbackEventArgs args)
    {
        string sId = args.Model.ContentId;

        if (string.IsNullOrWhiteSpace(sId))
        {
            args.Cancel = true;
            return;
        }

        //EventPageViewModel vm = new();
        //this.Pages.Add(vm);
        //args.Content = vm;

        ///TODO: Do an activator create instance + a loader for open documents
        string x = args.Model.ContentId;
    }

    private void OnLoadLayout(DockingManager? manager)
    {
        if (manager is null)
        {
            return;
        }

        string layoutFileName = Path.Combine(App.AppDataDirectory, "Layout.config");

        if (!File.Exists(layoutFileName))
        {
            this.CreateDefaultLayout();
            return;
        }

        XmlLayoutSerializer serializer = new(manager);

        serializer.LayoutSerializationCallback += (_, args) =>
        {
            if (args.Model.ContentId is null)
            {
                args.Cancel = true;
                return;
            }

            this.ReloadContentOnStartup(args);
        };

        serializer.Deserialize(layoutFileName);
    }
}