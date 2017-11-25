using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    //View Model for Presenting
    partial class PresenterViewModel:NotificationObject
    {
        # region binding properity
        private int selectedPage;
        public int SelectedPage
        {
            get { return selectedPage; }
            set 
            {
                if (selectedPage != value)
                {
                    selectedPage = value;
                    RaisePropertyChanged("SelectedPage");
                } 
            }
        }

        private Object projectTree;
        public Object ProjectTree
        {
            get { return projectTree; }
            set 
            {
                if (projectTree != value)
                {
                    projectTree = value;
                    RaisePropertyChanged("ProjectTree");
                } 
            }
        }

        private Object currentPageShapes;
        public Object CurrentPageShapes
        {
            get { return currentPageShapes; }
            set 
            {
                if(currentPageShapes != value)
                {
                    currentPageShapes = value;
                    RaisePropertyChanged("CurrentPageShapes");
                }
            }
        }
        # endregion binding properity
    }
}
