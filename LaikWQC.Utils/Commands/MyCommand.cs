using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LaikWQC.Utils.Commands
{
    public class MyCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public MyCommand(Action execute) : this(execute, null) { }
        public MyCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? (() => true);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            _execute?.Invoke();
        }
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
    public class MyCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public MyCommand(Action<T> execute) : this(execute, null) { }
        public MyCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute ?? ((x) => true);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if(parameter is T)
                return _canExecute((T)parameter);
            return _canExecute(default(T));
        }
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            if (parameter is T)
                _execute?.Invoke((T)parameter);
            _execute?.Invoke(default(T));
        }
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
