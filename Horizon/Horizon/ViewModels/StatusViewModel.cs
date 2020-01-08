using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Horizon.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets whether a Starbound test instance is running or not.
        /// </summary>
        public bool IsRunningStarbound { get; set; } = false;

        /// <summary>
        /// The color of the status bar.
        /// </summary>
        public SolidColorBrush StatusBarColor
        {
            get
            {
                if (this.IsRunningStarbound)
                {
                    return App.StyleManager.StatusBarActiveColor;
                }
                else
                {
                    return App.StyleManager.BaseBackgroundColor;
                }
            }
        }

        /// <summary>
        /// The text that is displayed on the status bar.
        /// </summary>
        public string StatusState { get; private set; } = "Ready";

        /// <summary>
        /// Changes the status state text to the specified text string.
        /// </summary>
        /// <param name="text">
        /// The text to display on the status bar.
        /// </param>
        public void ChangeStatus(string text) => this.StatusState = text;

        /// <summary>
        /// Clears the status state. Will read "Ready" when cleared.
        /// </summary>
        public void ClearStatus() => this.StatusState = "Ready";
    }
}