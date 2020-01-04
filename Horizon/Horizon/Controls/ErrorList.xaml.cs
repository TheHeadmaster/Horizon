using Horizon.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ErrorList.xaml
    /// </summary>
    public partial class ErrorList : UserControl
    {
        public ObservableCollection<Error> Errors { get; set; } = new ObservableCollection<Error>();

        public ErrorList()
        {
            this.InitializeComponent();
            this.DataContext = this;
            ErrorListener.ListenerUpdated += this.ErrorListener_ListenerUpdated;
            ErrorListener.Update();
        }

        private void ErrorListener_ListenerUpdated(ListenerUpdateEventArgs args)
        {
            this.Errors.Clear();
            foreach (Error error in ErrorListener.Errors)
            {
                this.Errors.Add(error);
            }
        }
    }
}