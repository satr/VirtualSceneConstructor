using System;
using System.Windows.Input;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// Base command
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        private Action _afterExecuteAction;

        /// <summary>
        /// /// Creates a new instance of a command
        /// </summary>
        protected CommandBase()
        {
        }

        /// <summary>
        /// Verify is the command can be executed
        /// </summary>
        /// <param name="parameter">Argument for the command</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the comand
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Execute();
            PerformAfterExecuteAction();
        }

        private void PerformAfterExecuteAction()
        {
            if (_afterExecuteAction != null)
                _afterExecuteAction();
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// Raises the event when the state CanExecute is changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// TODO
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Define the action performed after command is executed. Usual ussage - to close the view.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        /// <returns>Instance of this command.</returns>
        public ICommand AfterExecuteAction(Action action)
        {
            _afterExecuteAction = action;
            return this;
        }
    }
}