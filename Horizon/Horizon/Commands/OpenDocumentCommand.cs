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

namespace Horizon.Commands
{
    public class OpenDocumentCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new OpenDocumentCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            DocumentControl document = (DocumentControl)parameter;
            if (App.InterfaceData.Documents.Any(x => x.GetModel() == document))
            {
                DocumentControl model = App.InterfaceData.Documents.FirstOrDefault(x => x.GetModel() == document).GetModel();

                //MainWindow.Instance.FocusDocument(model);
                return;
            }
            DocumentControlViewModel newViewModel = document.CreateViewModel();
            App.InterfaceData.Documents.Add(newViewModel);

            //MainWindow.FocusDocument(newViewModel.GetModel());
        }
    }
}