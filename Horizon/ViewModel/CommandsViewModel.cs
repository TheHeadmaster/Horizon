using Horizon.ObjectModel;
using ReactiveUI;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;

namespace Horizon.ViewModel;

/// <summary>
/// Contains shared commands and properties between views that are logically related, so as not to duplicate code. This is
/// mainly only used for the workspace, main menu, and toolbar.
/// </summary>
public sealed class CommandsViewModel : ReactiveObject
{
    /// <inheritdoc />
    public CommandsViewModel()
    {
        this.NewProjectDialog = ReactiveCommand.CreateFromTask(this.HandleNewProjectDialog);
    }

    /// <summary>
    /// Command that opens a "new Project" dialog and waits for the interaction to complete.
    /// </summary>
    public ReactiveCommand<Unit, Unit> NewProjectDialog { get; set; }

    /// <summary>
    /// Interaction that handles the "New Project" dialog without blocking the UI.
    /// </summary>
    public Interaction<Unit, ProjectFile?> NewProjectDialogInteraction { get; } = new();

    /// <summary>
    /// handles the "New Project" dialog and creates a new project if the input is accepted.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    private async Task HandleNewProjectDialog()
    {
        ProjectFile? project = await this.NewProjectDialogInteraction.Handle(Unit.Default);

        if (project is not null)
        {
            await this.CreateNewProject(project);
        }
    }

    private async Task CreateNewProject(ProjectFile project)
    {
        if (!Directory.Exists(project.FilePath))
        {
            Directory.CreateDirectory(project.FilePath);
        }

        await project.Save();

        // await this.OpenProject(project);
    }
}