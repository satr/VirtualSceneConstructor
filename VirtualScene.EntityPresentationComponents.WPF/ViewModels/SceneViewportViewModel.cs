using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.EntityPresentationComponents.WPF.Models;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    ///     Scene viewport view model
    /// </summary>
    public class SceneViewportViewModel : ViewModelBase
    {
        private ImageSource _imageSource;
        private readonly SceneViewportModel _sceneViewportModel;

        /// <summary>
        /// Creates a new instance of the view-model of the main window of the application.
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public SceneViewportViewModel(ISceneContent sceneContent)
        {
            _sceneViewportModel = new SceneViewportModel(sceneContent);
            _sceneViewportModel.ImageSourceUpdated += (s, e) => ImageSource = e.ImageSource;
        }

        /// <summary>
        /// Context menu for viewports
        /// </summary>
        public IEnumerable<MenuItem> ViewportContextMenu
        {
            get { return _sceneViewportModel.ViewportContextMenu; }
        }


        /// <summary>
        /// SOurce of the image control
        /// </summary>
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (Equals(value, _imageSource))
                    return;
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Mouse down event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was put down</param>
        public void LeftButtonMouseDown(Point position)
        {
            HandleMouseEvent(_sceneViewportModel.Navigation.MouseDown, position);
        }

        /// <summary>
        /// Mouse up event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was up</param>
        public void LeftButtonMouseUp(Point position)
        {
            HandleMouseEvent(_sceneViewportModel.Navigation.MouseUp, position);
        }

        /// <summary>
        /// Mouse move event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was moved</param>
        public void LeftButtonMouseMove(Point position)
        {
            HandleMouseEvent(_sceneViewportModel.Navigation.MouseMove, position);
        }

        private static void HandleMouseEvent(Action<int, int> eventHandler, Point position)
        {
            eventHandler((int)position.X, (int)position.Y);
        }

        /// <summary>
        /// Notified when the view template as applied
        /// </summary>
        public void NotifyViewTemplateApplied()
        {
            _sceneViewportModel.SetupTimer();
        }
    }
}