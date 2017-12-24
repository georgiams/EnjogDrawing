using System.Xml;
namespace Model.EDAction
{
    class AddPageAction:EDAction
    {
        private string pageName;
        public AddPageAction (string name)
	    {
            Type = ActionType.AddPage;

            pageName = name;
	    }

        public override void Do(string projectXML)
        {
            XmlDocument xDoc=new XmlDocument();
            xDoc.InnerXml = projectXML;
            string page=string.Format("<Page name=\"{0}\"/>",pageName);
            xDoc.SelectSingleNode("/Project/Pages").InnerXml+=page;
        }

        public override void UnDo(string projectXML)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.InnerXml = projectXML;
            XmlNode pageNode= xDoc.SelectSingleNode(string.Format("/Project/Pages/Page[@name=\"{0}\"]",pageName));
            xDoc.RemoveChild(pageNode);
        }

        public override void ReDo(string projectXML)
        {
            Do(projectXML);
        }
    }
}
