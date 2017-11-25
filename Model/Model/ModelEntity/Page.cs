using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelEntity
{
    class Page
    {
        # region private properity
        public List<Shape> DrawedShapes { get; set; }
        public List<Object> UnDoStack { get; set; }
        public List<Object> ReDoStack { get; set; }
        # endregion private properity

        public Page()
        {
            DrawedShapes = new List<Shape>();
            UnDoStack = new List<object>();
            ReDoStack = new List<object>();
        }
    }
}
