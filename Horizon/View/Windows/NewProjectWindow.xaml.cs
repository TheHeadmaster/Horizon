using ReactiveUI;

namespace Horizon.View.Windows;

/// <summary>
/// Interaction logic for NewProjectWindow.xaml
/// </summary>
public partial class NewProjectWindow
{
    public NewProjectWindow()
    {
        this.InitializeComponent();

        this.ViewModel = new();

        this.WhenActivated(dispose =>
        {
        });
    }
}