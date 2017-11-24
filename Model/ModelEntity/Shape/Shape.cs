using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Model.ModelEntity
{
    enum ShapeType
    {
        Line,
        Circle,
        Rectangle
    }

    class ShapeFactory
    {
        public static Shape GetShape(ShapeType type, Point posA, Point posB)
        {
            switch (type)
            {
                case ShapeType.Line:
                    return new LineShape(posA, posB);
                    break;
                case ShapeType.Circle:
                    return new CircleShape(posA, posB);
                    break;
                case ShapeType.Rectangle:
                    return new RectangleShape(posA, posB);
                    break;
                default:
                    return null;
                    break;
            };
        }
    }

    abstract class Shape
    {
        public Point PosA { get; set; }
        public Point PosB { get; set; }
        public abstract void Show(Canvas canvas);
    }
}
