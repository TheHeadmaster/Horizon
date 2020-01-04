using FluentValidation.Results;
using Horizon.ViewModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class NewProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSaveEnabled => this.IsValid;

        public bool IsValid { get; private set; } = false;

        public string ProjectName { get; set; } = "";

        public string ProjectNameValidationText { get; set; } = "";

        public string ProjectPath { get; set; } = "";

        public string ProjectPathValidationText { get; set; } = "";

        public NewProjectValidation ValidationContext { get; } = new NewProjectValidation();

        public NewProjectViewModel()
        {
        }

        private void Validate()
        {
            ValidationResult results = this.ValidationContext.Validate(this);
            bool isValid = results.IsValid;
            ValidationFailure projectNameInvalid = results.Errors.FirstOrDefault(x => x.PropertyName == nameof(this.ProjectName));
            this.ProjectNameValidationText = projectNameInvalid is null ? "" : projectNameInvalid.ErrorMessage;

            ValidationFailure projectPathInvalid = results.Errors.FirstOrDefault(x => x.PropertyName == nameof(this.ProjectPath));
            this.ProjectPathValidationText = projectPathInvalid is null ? "" : projectPathInvalid.ErrorMessage;
            this.IsValid = isValid;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.Validate();
        }
    }
}