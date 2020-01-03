using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Horizon.Windows
{
    /// <summary>
    /// Allows the user to enable and disable mods, and launch the game.
    /// </summary>
    public partial class Launcher : Window
    {
        private static Launcher instance;

        public static Launcher Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new Launcher();
                }
                return instance;
            }
        }

        public LauncherViewModel ViewModel { get; set; }

        public Launcher()
        {
            this.InitializeComponent();
            this.ViewModel = new LauncherViewModel();
            this.DataContext = this.ViewModel;

            this.ModsListBox.ItemsSource = this.ViewModel.Mods;
        }
    }
}