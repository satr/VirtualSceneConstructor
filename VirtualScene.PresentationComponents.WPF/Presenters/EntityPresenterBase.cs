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
        /// TODO
        /// </summary>
        /// <param name="title"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        protected static Button CreateButton(string title, ICommand command)
        {
            var button = new Button
            {
                Width = 60,
                Height = 48,
                Content = title,
                Command = command
            };
            return button;
        }

    }
}