using System;
using System.Windows.Input;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Navigation in the scene
    /// </summary>
    public interface ISceneNavigation
    {
        /// <summary>
        /// Invoked when the keyboard's key has been got down
        /// </summary>
        /// <param name="keyEventArgs">Keys state</param>
        void KeyboardAction(KeyEventArgs keyEventArgs);

        /// <summary>
        /// Occurs when the object need to be moved
        /// </summary>
        event EventHandler<MoveEventArgs> Move;
    }
}