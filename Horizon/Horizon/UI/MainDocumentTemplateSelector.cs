using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Horizon.UI
{
    public class MainDocumentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CodeTemplate { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        public MainDocumentTemplateSelector() { }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ItemViewModel)
            {
                return this.ItemTemplate;
            }
            else if (item is CodeViewModel)
            {
                return this.CodeTemplate;
            }
            else
            {
                return base.SelectTemplate(item, container);
            }
        }
    }
}