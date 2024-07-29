using FluentValidation;

namespace Horizon.ViewModel.Validators;

public sealed class NewProjectWindowValidator : AbstractValidator<NewProjectWindowViewModel>
{
    public NewProjectWindowValidator()
    {
        this.RuleFor(vm => vm.Project.Name)
            .IsValidFolderName()
            .WithMessage("Name cannot be null, whitespace, and must be a valid folder name.");

        this.RuleFor(vm => vm.Project.FileDirectory)
            .IsValidPathName()
            .WithMessage("Path cannot be null, whitespace, and must be a valid path.")
            .IsEmptyPath()
            .WithMessage("Path must be empty or a non-existing folder.");
    }
}