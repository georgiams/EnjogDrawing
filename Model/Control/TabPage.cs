using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model.Control
{
    public class TabPage
    {
        # region properity
        public string Name { get; set; }
        public ObservableCollection<UIElement> Shapes { get; set; }
        # endregion properity

        public TabPage()
        {
            Name = string.Empty;
            Shapes = new ObservableCollection<UIElement>();
        }
    }
}
