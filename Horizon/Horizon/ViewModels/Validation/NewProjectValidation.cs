using FluentValidation;
using Horizon.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Horizon.ViewModels.Validation
{
    public class NewProjectValidation : AbstractValidator<NewProjectViewModel>
    {
        public NewProjectValidation()
        {
            this.RuleFor(x => x.ProjectName).Must(x => this.IsValidName(x))
                .WithMessage("Project name can only contain the characters [a-zA-Z0-9-_'] and whitespace, and must not be null.");
            this.RuleFor(x => x.ProjectPath).Must(x => this.IsValidPath(x)).WithMessage("Project path must be a valid absolute path.");
        }

        private bool IsValidName(string name)
        {
            if (name.IsNullOrWhitespace()) { return false; }
            Regex reg = new Regex(@"^[\w\d\s-\']+$");
            if (reg.IsMatch(name)) { return true; }
            else { return false; }
        }

        private bool IsValidPath(string path)
        {
            if (path.IsNullOrWhitespace()) { return false; }
            try
            {
                if (!Path.IsPathRooted(path)) { return false; }
                Path.GetFullPath(path);
            }
            catch
            {
                return false;
            }

            char[] invalidPathChars = Path.GetInvalidPathChars();

            if (path.Any(x => invalidPathChars.Contains(x))) { return false; }

            return true;
        }
    }
}