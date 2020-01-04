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

namespace Horizon.Commands
{
    public class PlayCommand : RelayCommand
    {
        private static Timer timer;

        public static Process StarboundRunning;

        private int FileLastPosition { get; set; } = 0;

        public static ICommand Instance { get; } = new PlayCommand();

        private void Proc_Exited(object sender, EventArgs args)
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    App.InterfaceData.IsRunningStarbound = false;
                    CommandManager.InvalidateRequerySuggested();
                });
            timer.Stop();
            timer.Dispose();
            this.FileLastPosition = 0;
            App.InterfaceData.StatusState = "Ready";
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            using (FileStream fs = File.Open(Path.Combine(App.CurrentProject.FilePath, "storage", "starbound.log"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(this.FileLastPosition, SeekOrigin.Begin);

                if (fs.Length < this.FileLastPosition) { return; }
                byte[] bytes = new byte[fs.Length - this.FileLastPosition];
                fs.Read(bytes, 0, bytes.Length);

                string s = Encoding.Default.GetString(bytes);

                App.InterfaceData.OutputText += s;
                this.FileLastPosition += bytes.Length;
            }
        }

        private void Watch()
        {
            timer = new Timer(2000);
            timer.Elapsed += this.Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public override bool CanExecute(object parameter) => App.InterfaceData.IsRunningStarbound == false;

        public override void Execute(object parameter)
        {
            App.InterfaceData.StatusState = "Starting Starbound...";
            App.InterfaceData.OutputText = "";
            Process proc = new Process();
            proc.StartInfo.FileName = Path.Combine(App.CurrentProject.FilePath, "win64", "starbound.exe");
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
            proc.EnableRaisingEvents = true;
            StarboundRunning = proc;
            App.InterfaceData.IsRunningStarbound = true;
            proc.Exited += this.Proc_Exited;
            App.InterfaceData.StatusState = "Starbound Running";
            this.Watch();
        }
    }
}