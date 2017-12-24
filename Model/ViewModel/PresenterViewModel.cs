using GalaSoft.MvvmLight.Messaging;
using Model.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml;

namespace Model.ViewModel
{
    //View Model for Presenting
    partial class ViewModel : NotificationObject
    {
        # region binding properity
        private int selectedPage;
        private List<TabPage> pages=new List<TabPage>();

        private ObservableCollection<NodeEntry> projectTree = new ObservableCollection<NodeEntry>();

        public ObservableCollection<NodeEntry> ProjectTree
        {
            get { return projectTree; }
            set
            {
                if (projectTree != value)
                {
                    projectTree = value;
                    RaisePropertyChanged("ProjectTree");
                }
            }
        }
        # endregion binding properity

        # region private method
        private void SendPagesChangeMessage()
        {
            Messenger.Default.Send<int>(selectedPage, "UpdateMessager");
            Messenger.Default.Send<List<TabPage>>(pages, "UpdateMessager");
        }
        
        private void UpdateViewModel(string projectXML)
        {
            
            pages=new List<TabPage>();
            projectTree = new ObservableCollection<NodeEntry>();

            ObservableCollection<NodeEntry> temProjectTree = new ObservableCollection<NodeEntry>();
            if (string.IsNullOrWhiteSpace(projectXML)) return;

            XmlDocument xDoc = new XmlDocument();
            xDoc.InnerXml=projectXML;
            if (xDoc == null) return;

            XmlNodeList pageNodeList = xDoc.SelectNodes("/Project/Pages/Page");

            int pageID = 0;
            foreach (XmlNode page in pageNodeList)
            {
                TabPage tb=new TabPage();
                tb.Name = page.Attributes["name"].Value;

                NodeEntry pageEntry = new NodeEntry();
                pageEntry.ID = pageID;
                pageEntry.Name = tb.Name;
                temProjectTree.Add(pageEntry);

                tb.Shapes=new ObservableCollection<UIElement>();
                XmlNodeList shapeNodeList = page.ChildNodes;
                int shapeID = 1024;
                foreach (XmlNode shape in shapeNodeList)
                {
                    string shapeName = shape.Attributes["name"].Value;
                    string shapeType = shape.Attributes["type"].Value;

                    NodeEntry shapeEntry = new NodeEntry();
                    shapeEntry.ID = shapeID;
                    shapeEntry.Name = shapeName;
                    shapeEntry.ParentID = pageID;
                    temProjectTree.Add(shapeEntry);

                    XmlNodeList pointNodeList=shape.ChildNodes;
                    List<Point> pointList = new List<Point>();
                    foreach (XmlNode point in pointNodeList)
                    {
                        string[] pointstr = point.InnerText.Split(',');
                        double p1 = double.Parse(pointstr[0]);
                        double p2 = double.Parse(pointstr[1]);
                        Point p=new Point(p1,p2);
                        pointList.Add(p);
                    }

                    if (string.Equals(shapeType, "Line")||
                        string.Equals(shapeType, "Rectangle")||
                        string.Equals(shapeType, "Circle"))
                    {
                        Point PosA = pointList[0];
                        Point PosB = pointList[1];
                        if (string.Equals(shapeType, "Line"))
                        {
                            Line l = new Line();
                            l.Stroke = System.Windows.Media.Brushes.Black;
                            l.X1 = PosA.X; l.Y1 = PosA.Y;
                            l.X2 = PosB.X; l.Y2 = PosB.Y;

                            tb.Shapes.Add(l);
                        }
                        else if (string.Equals(shapeType, "Rectangle"))
                        {
                            Rectangle r = new Rectangle();
                            r.Stroke = System.Windows.Media.Brushes.Black;
                            Vector v = PosA - PosB;
                            r.Width = Math.Abs(v.X);
                            r.Height = Math.Abs(v.Y);

                            r.SetValue(Canvas.LeftProperty, Math.Min(PosA.X, PosB.X));
                            r.SetValue(Canvas.TopProperty, Math.Min(PosA.Y, PosB.Y));

                            tb.Shapes.Add(r);
                        }
                        else if (string.Equals(shapeType, "Circle"))
                        {
                            Ellipse e = new Ellipse();
                            e.Stroke = System.Windows.Media.Brushes.Black;
                            double radius = (PosA - PosB).Length;
                            e.Width = 2 * radius;
                            e.Height = 2 * radius;
                            e.SetValue(Canvas.LeftProperty, (double)(PosA.X - radius));
                            e.SetValue(Canvas.TopProperty, (double)(PosA.Y - radius));

                            tb.Shapes.Add(e);
                        }
                        shapeID++;
                    }
                    pageID++;
                }
                pages.Add(tb);
            }
            SendPagesChangeMessage();

            ProjectTree = NodeEntry.Bind(temProjectTree);
        }
        # endregion private method

    }
}
