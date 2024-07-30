using ReactiveUI.Fody.Helpers;
using System.Windows.Media.Imaging;

namespace Horizon.ViewModel.Panes;

public sealed class ProjectExplorerViewModel : ToolViewModel
{
    public ProjectExplorerViewModel()
    {
        string path = "pack://application:,,,/Resources/Images/New.png";

        BitmapImage src = new();

        try
        {
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
        }
        catch (UriFormatException)
        {
        }

        this.IconSource = src;
    }

    [Reactive]
    public override string Title { get; set; } = "Project Explorer";
}