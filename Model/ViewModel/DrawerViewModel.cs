using Model.Action;
using Model.ModelController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    //View Model for Drawing
    partial class ViewModel:NotificationObject
    {
        # region binding properity
        private bool bDrawLine;
        public bool BDrawLine
        {
            get { return bDrawLine; }
            set 
            {
                if (bDrawLine != value)
                {
                    bDrawLine = value;
                    RaisePropertyChanged("BDrawLine");
                }
            }
        }

        private bool bDrawRectangle;
        public bool BDrawRectangle
        {
            get { return bDrawRectangle; }
            set
            {
                if (bDrawRectangle != value)
                {
                    bDrawRectangle = value;
                    RaisePropertyChanged("BDrawRectangle");
                }
            }
        }

        private bool bDrawCircle;
        public bool BDrawCircle
        {
            get { return bDrawCircle; }
            set
            {
                if (bDrawCircle != value)
                {
                    bDrawCircle = value;
                    RaisePropertyChanged("BDrawCircle");
                }
            }
        }
        # endregion binding properity

        # region binding command
        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand OpenCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand OpenShellCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }
        public DelegateCommand RodoCommand { get; set; }
        public DelegateCommand ShellInputCommand { get; set; }
        public DelegateCommand StartDrawCommand { get; set; }
        public DelegateCommand EndDrawCommand { get; set; }
        # endregion binding command

        # region private properity
        private ModelEntityController modelController = ModelEntityController.GetInstance();
        private ActionProcessor actionProcessor = ActionProcessor.GetInstance();
        # endregion private properity

        # region private method
        private void OperateCommand()
        {
            string actionsXML = GenerateActionsXML();
            string projectXML = ExecuteActions(actionsXML);
            UpdateViewModel(projectXML);
        }
        //根据Action和Shell生成Action XML
        private string GenerateActionsXML()
        {
            return null;
        }
        private string ExecuteActions(string actionsXML)
        {
            string projectXML=actionProcessor.ExecuteActions(actionsXML);
            return projectXML;
        }
        private void UpdateViewModel(string projectXML)
        {

        }
        # endregion private method
    }
}
