using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ModelEntity;
using System.Windows;
using System.Windows.Controls;

//Client Requirement
namespace Model.ModelController
{
    class ModelEntityController
    {
        # region singleton
        private static ModelEntityController _modelController;
        private ModelEntityController() 
        {
            NewPage();
        }
        static ModelEntityController()
        {
            _modelController = new ModelEntityController();
        }
        public static ModelEntityController GetInstance()
        {
            return _modelController;
        }
        # endregion singleton

        # region properity
        //Shape and page Data
        private ModelEntity.ModelEntity _modelEntity = ModelEntity.ModelEntity.GetInstance();
        //Current page
        public PageController CurrentPageController { get; set; }
        # endregion properity

        # region method
        public void NewPage()
        {
            CurrentPageController = new PageController();
        }
        public void VaryPage(PageController newPage)
        {
            CurrentPageController = newPage;
        }
        public void SavePage()
        {
            CurrentPageController.SavePage();
        }
        public void ClosePage()
        {
            CurrentPageController.SavePage();
            _modelEntity.RemovePage(CurrentPageController.Page);
        }
        public void Draw(Canvas canvas, ShapeType type, Point posA, Point posB)
        {
            CurrentPageController.Draw(canvas, type, posA, posB);
        }
        public void UnDo()
        {
            CurrentPageController.UnDo();
        }
        public void ReDo()
        {
            CurrentPageController.ReDo();
        }
        # endregion method
    }
}
