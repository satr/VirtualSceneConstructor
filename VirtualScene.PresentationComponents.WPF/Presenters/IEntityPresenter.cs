using System;
using System.Collections.Generic;
using System.Windows;

namespace VirtualScene.PresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Replesentation of the entity on UI
    /// </summary>
    public interface IEntityPresenter : IContentPresenter
    {
        /// <summary>
        /// The type of the entity
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// Control elements to operate with the entity
        /// </summary>
        IEnumerable<UIElement> TopElements { get; }
    }
}