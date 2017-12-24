using System.Xml;
namespace Model.EDAction
{
    class ActionFactory
    {
        public static EDAction CreateAction(string actionXML)
        {
            EDAction action = null;
            XmlDocument xDoc = new XmlDocument();
            xDoc.InnerXml = actionXML;
            XmlNode actionNode = xDoc.SelectSingleNode("/Action");
            string actionName = actionNode.Attributes["name"].Value;
            if (string.Equals(actionName,"addpage"))
            {
                string pageName = actionNode.ChildNodes[0].Attributes["name"].Value;
                action = new AddPageAction(pageName);
            }
            return action;
        }
    }
}
