using System;
using System.Windows.Input;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Navigation in the scene
    /// </summary>
    public class SceneNavigation : ISceneNavigation
    {
        /// <summary>
        /// Invoked when the keyboard's key has been got down
        /// </summary>
        /// <param name="keyEventArgs">Keys state</param>
        public void KeyboardAction(KeyEventArgs keyEventArgs)
        {
            const float movingStep = 0.2f;
            if (keyEventArgs.Key == Key.Up || keyEventArgs.Key == Key.W)
                OnMove(movingStep, 0f, 0f);
            if (keyEventArgs.Key == Key.Down || keyEventArgs.Key == Key.S)
                OnMove(-movingStep, 0f, 0f);
            if (keyEventArgs.Key == Key.Left || keyEventArgs.Key == Key.A)
                OnMove(0f, movingStep, 0f);
            if (keyEventArgs.Key == Key.Right || keyEventArgs.Key == Key.D)
                OnMove(0f, -movingStep, 0f);
            if (keyEventArgs.Key == Key.R || keyEventArgs.Key == Key.PageUp)
                OnMove(0f, 0f, movingStep);
            if (keyEventArgs.Key == Key.F || keyEventArgs.Key == Key.PageDown)
                OnMove(0f, 0f, -movingStep);
        }

        /// <summary>
        /// Occurs when the object need to be moved
        /// </summary>
        public event EventHandler<MoveEventArgs> Move;

        private void OnMove(float x, float y, float z)
        {
            var handler = Move;
            if (handler != null) 
                handler(this, new MoveEventArgs(x, y, z));
        }
    }
}