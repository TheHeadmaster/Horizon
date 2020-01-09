using Horizon.Windows;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ViewModels
{
    public class ProgramSelectionViewModel : ReactiveObject, IEnableLogger
    {
        public ReactiveCommand<Unit, Unit> SwitchToIDECommand { get; }

        public ReactiveCommand<Unit, Unit> SwitchToLauncherCommand { get; }

        public ProgramSelectionWindow View { get; }

        public ProgramSelectionViewModel(ProgramSelectionWindow view)
        {
            this.View = view;
            this.SwitchToLauncherCommand = ReactiveCommand.Create(this.SwitchToLauncher);
            this.SwitchToIDECommand = ReactiveCommand.Create(this.SwitchToIDE);
        }

        private void SwitchToIDE()
        {
            this.Log().Info("Selected IDE. Opening IDE window.");
            this.View.Close();
            new IDEWindow().Show();
            this.Log().Info("IDE window closed.");
        }

        private void SwitchToLauncher()
        {
            this.Log().Info("Selected launcher. Opening launcher window.");
            this.View.Close();
            new Launcher().Show();
            this.Log().Info("Launcher window closed.");
        }
    }
}