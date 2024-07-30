using FluentValidation.Results;
using Horizon.ObjectModel;
using Horizon.ViewModel.Validators;
using Nito.Disposables.Internals;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;

namespace Horizon.ViewModel;

public sealed class NewProjectWindowViewModel : ReactiveObject
{
    private readonly NewProjectWindowValidator validator = new();

    public NewProjectWindowViewModel()
    {
        this.WhenAnyValue(
            vm => vm.Project.Name,
            vm => vm.Project.FileDirectory)
            .Select(_ => this.validator.Validate(this))
            .ToPropertyEx(this, vm => vm.ValidationResult);

        this.WhenAnyValue(vm => vm.ValidationResult)
            .Select(x => x?.Errors.Where(error => error.PropertyName == "Project.Name")
                .WhereNotNull()
                .Select(error => error.ErrorMessage)
                .DefaultIfEmpty(string.Empty)
                .Aggregate((current, next) => $"{current}{Environment.NewLine}{next}"))
            .ToPropertyEx(this, vm => vm.NameError);

        this.WhenAnyValue(vm => vm.ValidationResult)
            .Select(x => x?.Errors.Where(error => error.PropertyName == "Project.FileDirectory")
                .WhereNotNull()
                .Select(error => error.ErrorMessage)
                .DefaultIfEmpty(string.Empty)
                .Aggregate((current, next) => $"{current}{Environment.NewLine}{next}"))
            .ToPropertyEx(this, vm => vm.PathError);

        this.WhenAnyValue(vm => vm.ValidationResult)
            .Select(x => x?.IsValid ?? true)
            .ToPropertyEx(this, vm => vm.IsValid);

        this.WhenAnyValue(
            vm => vm.SelectedTemplate)
            .Select(x => x is not null)
            .ToPropertyEx(this, vm => vm.CanPressProjectTemplateNext);
    }

    [ObservableAsProperty]
    public bool IsValid { get; }

    [ObservableAsProperty]
    public string NameError { get; } = string.Empty;

    [ObservableAsProperty]
    public string PathError { get; } = string.Empty;

    [ObservableAsProperty]
    public ValidationResult? ValidationResult { get; }

    [ObservableAsProperty]
    public bool CanPressProjectTemplateNext { get; }

    [Reactive]
    public ProjectTemplate? SelectedTemplate { get; set; }

    [Reactive]
    public ProjectFile Project { get; set; } = new() { Name = "Project Name", FilePath = Path.Combine(App.UserDirectory, "Project.horizon") };

    [Reactive]
    public ObservableCollection<ProjectTemplate> AvailableTemplates { get; init; } = [];
}