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
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace Model.Control
{
    /// <summary>
    /// Interaction logic for TabPageControl.xaml
    /// </summary>
    public partial class TabPageControl : UserControl
    {
        # region properity
        private int SelectedPageIndex=0;
        private List<TabPage> Pages=new List<TabPage>();
       # endregion properity

        public TabPageControl()
        {
            InitializeComponent();
            UpdateTabControl();

            Messenger.Default.Register<int>(this, "UpdateMessager", UpdateSelectedPage);
            Messenger.Default.Register<List<TabPage>>(this, "UpdateMessager", UpdatePages);
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private void UpdateSelectedPage(int selectedPageIndex)
        {
            SelectedPageIndex = selectedPageIndex;
            this.Dispatcher.Invoke(new Action(UpdateTabControl));
        }
        private void UpdatePages(List<TabPage> pages)
        {
            Pages = pages;
            this.Dispatcher.Invoke(new Action(UpdateTabControl));
            //UpdateTabControl();
        }
        private void UpdateTabControl()
        {
            this.InitializeComponent();
            TC.Items.Clear();

            for (int i = 0; i < Pages.Count; i++)
            {
                TabItem t = new TabItem();
                t.Header = Pages[i].Name;
                Canvas c = new Canvas();
                for (int j = 0; j < Pages[i].Shapes.Count; j++)
                {
                    c.Children.Add(Pages[i].Shapes[j]);
                }
                //c.Background = Brushes.Aqua;
                t.Content = c;
                TC.Items.Add(t);
            }
        }
    }
}
