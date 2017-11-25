using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Action
{
    class ActionProcessor
    {
        # region private properity
        private List<Action> actionList = new List<Action>();
        # endregion private properity

        # region public method
        public static ActionProcessor GetInstance()
        {
            return null;
        }
        public string ExecuteActions(string actionsXML)
        {
            Init(actionsXML);
            //Execute Actions
            return null;
        }
        # endregion public method

        # region private method
        private void Init(string actionsXML)
        {
            actionList.Clear();
            //Create Actions
        }
        # endregion private method
    }
}
