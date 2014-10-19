using System.Windows;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents data-content on the UI
    /// </summary>
    public interface IContentPresenter
    {
        /// <summary>
        /// Content of the entity
        /// </summary>
        /// <returns>The view with the content of the entity</returns>
        FrameworkElement GetContentView();

        /// <summary>
        /// The content of the scene
        /// </summary>
        ISceneContent SceneContent { set; }
    }
}