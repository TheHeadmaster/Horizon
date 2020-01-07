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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Horizon.Controls
{
    /// <summary>
    /// Toolbar used to access various commands and functions.
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public static MainMenu Instance { get; private set; }

        public MainMenuViewModel ViewModel { get; set; }

        public MainMenu()
        {
            this.InitializeComponent();
            Instance = this;

            this.ViewModel = new MainMenuViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}