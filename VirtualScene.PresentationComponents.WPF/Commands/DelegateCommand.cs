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
        /// Creates a new instance of the command invoking the action
        /// </summary>
        /// <param name="action">The action to be invoked</param>
        public DelegateCommand(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// The command execution routine
        /// </summary>
        protected override void Execute()
        {
            _action();
        }
    }
}