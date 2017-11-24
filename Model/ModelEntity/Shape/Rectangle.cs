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
    class RectangleShape:Shape
    {
        public RectangleShape(Point posA, Point posB)
        {
            this.PosA = posA;
            this.PosB = posB;
        }
        public override void Show(Canvas canvas)
        {
            Rectangle r = new Rectangle();
            r.Stroke = System.Windows.Media.Brushes.Black; 
            Vector v = PosA-PosB;
            r.Width = Math.Abs(v.X);
            r.Height = Math.Abs(v.Y);

            r.SetValue(Canvas.LeftProperty, Math.Min(PosA.X,PosB.X));
            r.SetValue(Canvas.TopProperty, Math.Min(PosA.Y, PosB.Y));
            canvas.Children.Add(r);
        }
    }
}
