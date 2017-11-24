using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Model.ModelEntity
{
    class CircleShape:Shape
    {
        public CircleShape(Point posA, Point posB)
        {
            this.PosA = posA;
            this.PosB = posB;
        }
        public override void Show(Canvas canvas)
        {
            Ellipse e = new Ellipse();
            e.Stroke = System.Windows.Media.Brushes.Black;
            double radius = (PosA - PosB).Length;
            e.Width = 2 * radius;
            e.Height = 2 * radius;
            e.SetValue(Canvas.LeftProperty, (double)(PosA.X-radius));
            e.SetValue(Canvas.TopProperty, (double)(PosA.Y - radius));
            canvas.Children.Add(e);
        }
    }
}
