using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Model.ModelEntity
{
    class LineShape:Shape
    {
        public LineShape(Point posA, Point posB)
        {
            this.PosA = posA;
            this.PosB = posB;
        }
        public override void Show(System.Windows.Controls.Canvas canvas)
        {
            Line l = new Line();
            l.Stroke = System.Windows.Media.Brushes.Black;
            l.X1 = PosA.X; l.Y1 = PosA.Y;
            l.X2 = PosB.X; l.Y2 = PosB.Y;
            canvas.Children.Add(l);
        }
    }
}
