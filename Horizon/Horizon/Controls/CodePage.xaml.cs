using Horizon.ObjectModel;
using Horizon.UI;
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
using System.IO;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Editing;

namespace Horizon.Controls
{
    /// <summary>
    /// Interaction logic for CodePage.xaml
    /// </summary>
    public partial class CodePage : UserControl
    {
        private FoldingManager foldingManager;

        private LuaFoldingStrategy foldingStrategy = new LuaFoldingStrategy();

        public static readonly DependencyProperty ViewModelProperty =
                         DependencyProperty.Register("ViewModel", typeof(CodeViewModel), typeof(CodePage), new
      PropertyMetadata(default(CodeViewModel), new PropertyChangedCallback(OnViewModelChanged)));

        public CodeViewModel ViewModel
        {
            get => (CodeViewModel)this.GetValue(ViewModelProperty);
            set => this.SetValue(ViewModelProperty, value);
        }

        public CodePage()
        {
            this.InitializeComponent();
            using (Stream s = new MemoryStream(Properties.Resources.Lua))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {
                    this.TEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            this.TEditor.TextArea.LeftMargins.Add(new LineNumberMargin { TextView = this.TEditor.TextArea.TextView });
            this.TEditor.TextArea.LeftMargins.Add(new FoldingMargin { TextView = this.TEditor.TextArea.TextView });
        }

        private static void OnViewModelChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs args)
        {
            CodePage itemPageControl = d as CodePage;
            itemPageControl.OnViewModelChanged(args);
        }

        private void OnViewModelChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        private void TEditor_TextChanged(object sender, EventArgs e)
        {
            if (this.TEditor.Document != null && this.foldingManager != null)
            {
                this.foldingStrategy.UpdateFoldings(this.foldingManager, this.TEditor.Document);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.foldingManager = FoldingManager.Install(this.TEditor.TextArea);
            this.foldingStrategy.UpdateFoldings(this.foldingManager, this.TEditor.Document);
        }
    }
}