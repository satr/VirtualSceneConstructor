using System.Windows;
using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.PresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents data-content on UI
    /// </summary>
    public abstract class ContentPresenterBase: IContentPresenter
    {
        private FrameworkElement _contentView;

        /// <summary>
        /// The content of the scene
        /// </summary>
        public ISceneContent SceneContent { get; set; }

        /// <summary>
        /// Content of the entity
        /// </summary>
        /// <returns>The view with the content of the entity</returns>
        public FrameworkElement GetContentView()
        {
            return _contentView ?? (_contentView = CreateContentView());
        }

        /// <summary>
        /// Build the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected abstract FrameworkElement CreateContentView();
    }
}