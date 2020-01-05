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
    /// Shows the status of current operations and other at-a-glance data.
    /// </summary>
    public partial class Status : UserControl
    {
        public static Status Instance { get; private set; }

        public StatusViewModel ViewModel { get; set; }

        public Status()
        {
            this.InitializeComponent();
            Instance = this;

            this.ViewModel = new StatusViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}