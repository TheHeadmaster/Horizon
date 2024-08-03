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
        this.OpenProjectDialog = ReactiveCommand.CreateFromTask(this.HandleOpenProjectDialog);
    }

    /// <summary>
    /// Command that opens a "new Project" dialog and waits for the interaction to complete.
    /// </summary>
    public ReactiveCommand<Unit, Unit> NewProjectDialog { get; set; }

    /// <summary>
    /// Command that opens an "Open Project" file picker and waits for the interaction to complete.
    /// </summary>
    public ReactiveCommand<Unit, Unit> OpenProjectDialog { get; set; }

    /// <summary>
    /// Interaction that handles the "Open Project" dialog without blocking the UI.
    /// </summary>
    public Interaction<Unit, ProjectFile?> OpenProjectDialogInteraction { get; } = new();

    /// <summary>
    /// Interaction that handles the "New Project" dialog without blocking the UI.
    /// </summary>
    public Interaction<Unit, ProjectFile?> NewProjectDialogInteraction { get; } = new();

    /// <summary>
    /// Closes the current <see cref="ProjectFile" />.
    /// </summary>
    public static void CloseCurrentProject()
    {
        App.ViewModel.CurrentProject = null;
    }

    /// <summary>
    /// Loads the specified <see cref="ProjectFile" /> and sets it as the current project.
    /// </summary>
    /// <param name="project">The <see cref="ProjectFile" /> to load.</param>
    public static async Task LoadProject(ProjectFile project)
    {
        App.ViewModel.CurrentProject = project;
        await App.ViewModel.CurrentProject.Load();
    }

    /// <summary>
    /// Creates a new <see cref="ProjectFile" /> on disk and opens it.
    /// </summary>
    /// <param name="project">
    /// A <see cref="ProjectFile" /> object with initial data such as save path, used as a seed for the new project.
    /// </param>
    private static async Task CreateNewProject(ProjectFile project)
    {
        if (!Directory.Exists(project.FileDirectory))
        {
            Directory.CreateDirectory(project.FileDirectory);
        }

        await project.Save();

        await OpenProject(project);
    }

    /// <summary>
    /// Opens an existing <see cref="ProjectFile" /> on disk.
    /// </summary>
    /// <param name="project">A <see cref="ProjectFile" /> object loaded from disk.</param>
    private static async Task OpenProject(ProjectFile project)
    {
        CloseCurrentProject();
        await LoadProject(project);
    }

    /// <summary>
    /// Handles the "Open Project" dialog and opens the selected project if the input is accepted.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    private async Task HandleOpenProjectDialog()
    {
        ProjectFile? project = await this.OpenProjectDialogInteraction.Handle(Unit.Default);

        if (project is not null)
        {
            await OpenProject(project);
        }
    }

    /// <summary>
    /// handles the "New Project" dialog and creates a new project if the input is accepted.
    /// </summary>
    /// <returns>An awaitable <see cref="Task" />.</returns>
    private async Task HandleNewProjectDialog()
    {
        ProjectFile? project = await this.NewProjectDialogInteraction.Handle(Unit.Default);

        if (project is not null)
        {
            await CreateNewProject(project);
        }
    }
}