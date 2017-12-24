using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EDAction
{
    class AddShapeAction:EDAction
    {
        # region properity
        # endregion properity

        # region method
        public AddShapeAction()
        {
            Type = ActionType.AddShape;
        }
        
        

        public override void Do(string projectXML)
        {
            throw new NotImplementedException();
        }

        public override void UnDo(string projectXML)
        {
            throw new NotImplementedException();
        }

        public override void ReDo(string projectXML)
        {
            throw new NotImplementedException();
        }
        # endregion method
    }
}
