using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelEntity
{
    class ModelEntity
    {
        #region private properity
        private List<Page> _pages=new List<Page>();
        private static ModelEntity _modelEntity;
        #endregion private properity

        # region private method
        private ModelEntity()
        {
            _pages.Clear();
        }
        #endregion private method

        # region public method
        public Page AddPage(Page p)
        {
            _pages.Add(p);
            return p;
        }

        public void RemovePage(Page p)
        {
            int index = _pages.IndexOf(p);
            _pages.RemoveAt(index);
        }
        # endregion public method

        # region static method
        static ModelEntity()
        {
            _modelEntity = new ModelEntity();
        }

        public static ModelEntity GetInstance()
        {
             return _modelEntity;
        }
        # endregion static method 
    }
}
