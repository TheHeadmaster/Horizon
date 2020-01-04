using Horizon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using Microsoft.Win32;
using Horizon.Controls;

namespace Horizon.Windows
{
    /// <summary>
    /// The main window for the IDE.
    /// </summary>
    public partial class IDEWindow : BorderlessWindow
    {
        public IDEWindow()
        {
            this.InitializeComponent();
        }
    }
}