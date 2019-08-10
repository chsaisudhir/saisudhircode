using System;
using System.Windows.Input;

namespace NumberToWordConverter
{
    public class RelayCommand : ICommand
    {
        Action<object> _execteMethod;
        Predicate<object> _canexecuteMethod;


        public RelayCommand(Action<object> execteMethod, Predicate<object> canexecuteMethod)
        {
            _execteMethod = execteMethod;
            _canexecuteMethod = canexecuteMethod;
        }

        internal void RaiseCanExecuteChanged(object parameter)
        {
            CanExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canexecuteMethod != null)
            {
                return _canexecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {

                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execteMethod(parameter);
        }
    }
}
