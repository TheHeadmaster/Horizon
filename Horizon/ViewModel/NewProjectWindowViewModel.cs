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
            vm => vm.Project!.Name,
            vm => vm.Project!.FilePath)
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

        this.WhenAnyValue(
            vm => vm.SelectedTemplate)
            .Select(x =>
            {
                if (x is not null)
                {
                    var instance = (ProjectFile?)Activator.CreateInstance(x.GetType());
                    if (instance is not null)
                    {
                        instance.Name = "New Project";
                        instance.FilePath = Path.Combine(App.UserDirectory, "Project.horizon");
                    }

                    return instance;
                }

                return null;
            })
            .Subscribe(x =>
            {
                this.Project = x;
            });
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
    public ProjectFile? SelectedTemplate { get; set; }

    [Reactive]
    public ProjectFile? Project { get; set; }

    [Reactive]
    public string ProjectName { get; set; } = "New Project";

    [Reactive]
    public string ProjectPath { get; set; } = Path.Combine(App.UserDirectory, "Project.horizon");

    [Reactive]
    public ObservableCollection<ProjectFile> AvailableTemplates { get; set; }
}