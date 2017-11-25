namespace Model.Action
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
    abstract class Action
    {
        # region properity
        public ActionType Type { get; set; }
        public string Name { get; set; }
        # endregion properity

        # region method
        public abstract void Do();
        public abstract void UnDo();
        public abstract void ReDo();
        # endregion method
    }
}
