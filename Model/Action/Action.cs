namespace Model.EDAction
{
    enum ActionType
    {
        AddPage,
        RemovePage,
        AddShape,
        RemoveShape,
        ShowProject,
        ShowPage,
        SaveProject,
        CloseProject
    }
    abstract class EDAction
    {
        # region properity
        public ActionType Type { get; set; }
        public string Name { get; set; }
        # endregion properity

        # region method
        public abstract void Do(string projectXML);
        public abstract void UnDo(string projectXML);
        public abstract void ReDo(string projectXML);
        # endregion method
    }
}
