using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VirtualScene.PresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents the entity on UI
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    public abstract class EntityPresenterBase<T> : ContentPresenterBase, IEntityPresenter
    {
        /// <summary>
        /// Control elements to operate with the entity
        /// </summary>
        public virtual IEnumerable<UIElement> TopElements {
            get { yield break; }
        }

        /// <summary>
        /// The type of the entity
        /// </summary>
        public Type EntityType { get { return typeof(T); } }


        /// <summary>
        /// Create a button for an operation.
        /// </summary>
        /// <param name="title">Title of the button.</param>
        /// <param name="command">The command executed by the button.</param>
        /// <returns>The UI control executing the operation.</returns>
        protected static UIElement CreateButton(string title, ICommand command)
        {
            var button = new Button
            {
                Width = 60,
                Height = 48,
                Content = title,
                Command = command,
                ToolTip = title
            };
            return button;
        }

    }
}