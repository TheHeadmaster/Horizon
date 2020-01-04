using Horizon.UI;
using Horizon.ViewModels;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ObjectModel
{
    public class Code : DocumentControl
    {
        public TextDocument Document { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FullPath => Path.Combine(this.FilePath, this.FileName);

        public override string Header => this.FileName;

        public IHighlightingDefinition Highlighting { get; set; }

        public Code(string fullPath)
        {
            this.FilePath = Path.GetDirectoryName(fullPath);
            this.FileName = Path.GetFileName(fullPath);
        }

        public override DocumentControlViewModel CreateViewModel() => new CodeViewModel { Model = this };

        public override List<Error> EvaluateErrors() => new List<Error>();

        public void Load()
        {
            if (!Directory.Exists(this.FilePath))
            {
                Directory.CreateDirectory(this.FilePath);
            }
            if (!File.Exists(this.FullPath))
            {
                return;
            }
            else
            {
                using (FileStream fs = new FileStream(this.FullPath,
                           FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = FileReader.OpenStream(fs, Encoding.UTF8))
                    {
                        this.Document = new TextDocument(reader.ReadToEnd());
                    }
                }
            }
        }

        public void Save()
        {
            if (!Directory.Exists(this.FilePath))
            {
                Directory.CreateDirectory(this.FilePath);
            }
            File.WriteAllText(this.FullPath, this.Document.Text);
        }
    }
}