using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Epic.MVVM.Commands
{
    public class DelegateCommand : ICommand
    {


        public static DelegateCommand<T> Create<T>(Action<T> execute = null, Func<T, bool> canExecute = null)
        {
            return new DelegateCommand<T>(execute, canExecute);
        }

        public DelegateCommand(Action<object> execute = null, Func<object, bool> canExecute = null)
        {
            this.ExecuteCommand = execute;
            this.CanExecuteCommand = canExecute;
        }

        public Action<object> ExecuteCommand { get; set; }
        public Func<object, bool> CanExecuteCommand { get; set; }

        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteCommand == null) return true;
            return this.CanExecuteCommand(parameter);
        }


        public void Execute(object parameter)
        {
            if (this.ExecuteCommand == null) return;
            this.ExecuteCommand(parameter);
        }

        #region Event

        public event EventHandler CanExecuteChanged;


        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged == null) return;
            this.CanExecuteChanged(this, EventArgs.Empty);
        }

        #endregion

    }


    public class DelegateCommand<T> : ICommand
    {

        public DelegateCommand(Action<T> execute = null, Func<T, bool> canExecute = null)
        {
            this.ExecuteCommand = execute;
            this.CanExecuteCommand = canExecute;
        }

        public Action<T> ExecuteCommand { get; set; }
        public Func<T, bool> CanExecuteCommand { get; set; }

        public bool CanExecute(T parameter)
        {
            if (this.CanExecuteCommand == null) return true;
            return this.CanExecuteCommand(parameter);
        }


        public void Execute(T parameter)
        {
            if (this.ExecuteCommand == null) return;
            this.ExecuteCommand(parameter);
        }

        #region ICommand

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute((T)parameter);
        }

        #endregion

        #region Event

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged == null) return;
            this.CanExecuteChanged(this, EventArgs.Empty);
        }

        #endregion
    }
}
