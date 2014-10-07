using System.Windows;

namespace VirtualScene.Application.WPF.Views
{
    /// <summary>
    /// The interface of the main application view
    /// </summary>
    public interface IMainWindowView
    {
        /// <summary>
        /// Adds a UI controll element to the main view
        /// </summary>
        /// <param name="uiElement">UI control element</param>
        void AddTopElement(UIElement uiElement);

        /// <summary>
        /// Occurs when a key is pressed while focus is on this element.
        /// </summary>
        event System.Windows.Input.KeyEventHandler KeyDown;
    }
}