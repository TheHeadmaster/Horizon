using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Horizon.UI
{
    public class StyleManager
    {
        public SolidColorBrush AccentBackgroundColor => App.Current.Resources["AccentBackgroundColor"] as SolidColorBrush;

        public SolidColorBrush BaseBackgroundColor => App.Current.Resources["BaseBackgroundColor"] as SolidColorBrush;

        public SolidColorBrush StatusBarActiveColor => App.Current.Resources["StatusBarActiveColor"] as SolidColorBrush;
    }
}