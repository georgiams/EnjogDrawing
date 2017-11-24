using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ModelEntity;

namespace Model.Action
{
    class DeleteShapeAction:Action
    {
        # region properity
        public Page parentPage { get; set; }
        public Shape currentShape { get; set; }
        # endregion properity
        # region method
        public DeleteShapeAction()
        {
            type = ActionType.DeleteShape;
        }
        public override void Do()
        {

        }
        public override void UnDo()
        {

        }
        public override void ReDo()
        {

        }
        # endregion method
    }
}
