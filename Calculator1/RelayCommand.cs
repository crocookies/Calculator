using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator1
{
    public class RelayCommand : ICommand
    {
        private readonly Action<Object> execute;
        private readonly Func<object, bool> canExcute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExcute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExcute = canExcute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return canExcute == null || canExcute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
