using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Presents a splash window that disappears in 5 seconds.
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Waits for 5 seconds, then closes the window and opens program selection on the UI thread.
        /// </summary>
        private void DelayThread()
        {
            Thread.Sleep(5000);
            this.Dispatcher.Invoke(() =>
            {
                bool? _ = new ProgramSelectionWindow().ShowDialog();
                this.Close();
            });
        }

        /// <summary>
        /// Calls the delay thread when the window loads.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="args">
        /// The event arguments.
        /// </param>
        private void SplashLoaded(object sender, RoutedEventArgs args) => new Thread(new ThreadStart(this.DelayThread)).Start();
    }
}