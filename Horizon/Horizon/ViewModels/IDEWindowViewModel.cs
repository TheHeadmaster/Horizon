using Horizon.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Horizon.ViewModels
{
    /// <summary>
    /// ViewModel for the IDE Window.
    /// </summary>
    public class IDEWindowViewModel : ReactiveObject
    {
        /// <summary>
        /// Gets or sets the current project.
        /// </summary>
        [Reactive]
        public Project CurrentProject { get; set; }
    }
}