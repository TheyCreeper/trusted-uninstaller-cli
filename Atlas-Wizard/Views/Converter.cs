using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using static TrustedUninstaller.Shared.Playbook;

namespace Atlas_Wizard.Views
{
    public class PageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RadioPage { get; set; }
        public DataTemplate CheckboxPage { get; set; }
        public DataTemplate RadioImagePage { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CheckboxPage)
            {
                return CheckboxPage;
            }
            if (item is RadioPage)
            {
                return RadioPage;
            }
            if (item is RadioImagePage)
            {
                return RadioImagePage;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
