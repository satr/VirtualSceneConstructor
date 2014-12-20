using System;
using System.Windows.Input;
using VirtualScene.BusinessComponents.Core.Events;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// Navigation in the scene
    /// </summary>
    public interface ISceneNavigator
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