using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Action
{
    enum ActionType
    {
        DeleteShape,
        AddShape
    }
    abstract class Action
    {
        # region properity
        public ActionType type { get; set; }
        # endregion properity

        # region method
        public abstract void Do();
        public abstract void UnDo();
        public abstract void ReDo();
        # endregion method
    }
}
