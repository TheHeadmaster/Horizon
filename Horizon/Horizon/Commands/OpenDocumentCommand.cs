using Horizon.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Horizon.ViewModels;
using System.Windows.Documents;
using Horizon.Windows;
using Horizon.Diagnostics;
using Horizon.Controls;

namespace Horizon.Commands
{
    /// <summary>
    /// Opens a document.
    /// </summary>
    public class OpenDocumentCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new OpenDocumentCommand();

        public override bool CanExecute(object parameter) => true;

        [Log("Opening document...", ExitMessage = "Document opened.")]
        public override void Execute(object parameter)
        {
            DocumentControl document = (DocumentControl)parameter;
            if (Workspace.Instance.ViewModel.Documents.Any(x => x.GetModel() == document))
            {
                DocumentControl model = Workspace.Instance.ViewModel.Documents.FirstOrDefault(x => x.GetModel() == document).GetModel();

                //TODO: Fix functionality for focusing document
                //IDEWindow.Instance.ViewModel.FocusDocument(model);
                return;
            }
            DocumentControlViewModel newViewModel = document.CreateViewModel();

            //Workspace.Instance.ViewModel.Documents.Add(newViewModel);

            //MainWindow.FocusDocument(newViewModel.GetModel());
        }
    }
}