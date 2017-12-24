using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model.EDAction
{
    class ModelProcessor
    {
        # region for singleton
        private static ModelProcessor instance = null;
        private ModelProcessor(){}
        public static ModelProcessor GetInstance()
        {
            if (instance == null)
            {
                instance = new ModelProcessor();
            }
            return instance;
        }
        # endregion for singleton

        # region private properity
        private string modelXML = string.Empty;

        private List<EDAction> temActionList = new List<EDAction>();
        private Action<string> executeActionList = null;

        private Stack<EDAction> undoActionStack = new Stack<EDAction>();
        private Stack<EDAction> redoActionStack = new Stack<EDAction>();
        # endregion private properity

        # region public method
        public string ExecuteActions(string actionsXML, ref string projectXml)
        {
            ParseActions(actionsXML);
            modelXML = projectXml;
            //Execute Actions
            executeActionList(projectXml);

            projectXml=modelXML;
            return null;
        }
        # endregion public method

        # region private method
        private void ParseActions(string actionsXML)
        {
            temActionList.Clear();
            //Create Actions
            XmlDocument xDoc = new XmlDocument();
            xDoc.InnerXml = actionsXML;
            XmlNode actionNode = xDoc.SelectSingleNode("/Action");

            string actionType = actionNode.Attributes["type"].Value;
            EDAction action = null;
            if (string.Equals("undo",actionType))
            {
                action = undoActionStack.Pop();
                executeActionList += action.UnDo;
                redoActionStack.Push(action);
                return;
            }
            if (string.Equals("redo", actionType))
            {
                action = redoActionStack.Pop();
                executeActionList += action.ReDo;
                undoActionStack.Push(action);
                return;
            }
            action = ActionFactory.CreateAction(actionsXML);
            executeActionList += action.Do;
            undoActionStack.Push(action);
        }
        # endregion private method
    }
}
