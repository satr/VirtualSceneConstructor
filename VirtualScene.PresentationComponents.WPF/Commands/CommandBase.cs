using System;
using System.Windows.Input;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// Base command
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// VirtualSceneContext 
        /// </summary>
        protected SceneContent SceneContent { get; set; }

        /// <summary>
        /// /// Creates a new instance of a command
        /// </summary>
        /// <param name="sceneContent"></param>
        protected CommandBase(SceneContent sceneContent)
        {
            SceneContent = sceneContent;
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
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}