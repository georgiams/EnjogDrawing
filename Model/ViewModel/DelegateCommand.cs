using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Model.ViewModel
{
    class DelegateCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (null != CanExecuteFunc)
                CanExecuteFunc(parameter);
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            if (null != ExecuteAction)
                ExecuteAction(parameter);
        }
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }
    }
}
