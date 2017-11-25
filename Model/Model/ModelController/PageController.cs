using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ModelEntity;
using System.Windows;
using System.Windows.Controls;

namespace Model.ModelController
{
    class PageController
    {
        # region properity
        public Model.ModelEntity.Page Page { get; set; }
        # endregion properity

        # region method
        public PageController()
        {
            Page = new ModelEntity.Page();
        }
        public void Draw(Canvas canvas, ShapeType type, Point posA, Point posB)
        {
            Shape newShape=ShapeFactory.GetShape(type, posA, posB);
            newShape.Show(canvas);
            Page.DrawedShapes.Add(newShape);

            //Add UnDo stack
        }
        public void UnDo()
        {

        }
        public void ReDo()
        {

        }
        public void SavePage()
        {

        }
        # endregion method
    }
}
