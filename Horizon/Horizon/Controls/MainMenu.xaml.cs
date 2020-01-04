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
        private static MainMenu instance;

        public static MainMenu Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new MainMenu();
                }
                return instance;
            }
        }

        //public MainMenuViewModel ViewModel { get; set; }

        public MainMenu()
        {
            this.InitializeComponent();

            //this.ViewModel = new MainMenuViewModel();
            //this.DataContext = this.ViewModel;
        }
    }
}