using System;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command invoking the action
    /// </summary>
    public class DelegateCommand: CommandBase
    {
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" />
        /// </summary>
        /// <param name="action">The action to be invoked</param>
        public DelegateCommand(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// The command execution routine
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            _action();
        }
    }
}