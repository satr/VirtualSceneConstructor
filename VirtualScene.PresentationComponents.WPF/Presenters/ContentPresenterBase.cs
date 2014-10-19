using System.Windows;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents data-content on UI
    /// </summary>
    public abstract class ContentPresenterBase: IContentPresenter
    {
        /// <summary>
        /// The content of the scene
        /// </summary>
        public ISceneContent SceneContent { get; set; }

        /// <summary>
        /// Content of the entity
        /// </summary>
        /// <returns>The view with the content of the entity</returns>
        public abstract FrameworkElement GetContentView();
    }
}