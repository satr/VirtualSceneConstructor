using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The model of the scene viewport
    /// </summary>
    public class SceneViewportModel
    {
        private bool _disposed;
        private readonly SceneViewControl _sceneViewControl;
        private readonly SceneViewport _sceneViewport;
        private readonly SceneContent _sceneContent;

        /// <summary>
        /// Create ethe new instance of the viewport
        /// </summary>
        /// <param name="sceneViewControl"></param>
        /// <param name="sceneContent"></param>
        public SceneViewportModel(SceneViewControl sceneViewControl, SceneContent sceneContent)
        {
            _sceneViewControl = sceneViewControl;
            _sceneViewControl.DrawFPS = true;
            _sceneContent = sceneContent;
            _sceneViewport = _sceneContent.SceneEngine.CreateViewport();
            _sceneViewControl.Scene = _sceneViewport.Scene;
            _sceneViewControl.Camera = _sceneViewport.Scene.CurrentCamera;
            InitViewportContextMenu(_sceneContent);
            Bind();
        }

        private void Bind()
        {
            if (_sceneViewControl == null)
                return;
            _sceneViewControl.MouseDown += ViewportMouseDown;
            _sceneViewControl.MouseUp += ViewportMouseUp;
            _sceneViewControl.MouseMove += ViewportMouseMove;
            _sceneContent.SceneEngine.Cameras.CollectionChanged += CamerasCollectionChanged;
        }

        private void UnBind()
        {
            if(_sceneViewControl == null)
                return;
            _sceneViewControl.MouseDown -= ViewportMouseDown;
            _sceneViewControl.MouseUp -= ViewportMouseUp;
            _sceneViewControl.MouseMove -= ViewportMouseMove;
            _sceneContent.SceneEngine.Cameras.CollectionChanged -= CamerasCollectionChanged;
        }


        private void InitViewportContextMenu(SceneContent sceneContent)
        {
            ViewportContextMenu = new ObservableCollection<MenuItem>();
            foreach (var camera in sceneContent.SceneEngine.Cameras)
                ViewportContextMenu.Add(CreateViewportMenuItemSelectCamera(camera));
        }

        private void CamerasCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Camera addedCamera in e.NewItems)
                {
                    ViewportContextMenu.Add(CreateViewportMenuItemSelectCamera(addedCamera));
                }
            }

            if (e.OldItems != null)
            {
                foreach (Camera removedCamera in e.OldItems)
                {
                    var menuItem = ViewportContextMenu.FirstOrDefault(mi => Equals(mi.Tag, removedCamera));
                    if (menuItem == null)
                        continue;
                    ViewportContextMenu.Remove(menuItem);
                }
            }
        }

        /// <summary>
        /// Context menu items for viewports
        /// </summary>
        public ObservableCollection<MenuItem> ViewportContextMenu { get; set; }

        private MenuItem CreateViewportMenuItemSelectCamera<T>(T camera)
            where T : Camera
        {
            var menuItem = new MenuItem
            {
                Header = string.Format(Resources.Title_Camera_N, camera.Name),
                Tag = camera,
                Command = new SetCameraToSceneViewCommand(_sceneViewControl, camera),
            };
            return menuItem;
        }

        private void ViewportMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            NavigateCamera(_sceneViewport.Navigation.MouseDown, e);
        }

        private void ViewportMouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigateCamera(_sceneViewport.Navigation.MouseUp, e);
            Mouse.OverrideCursor = null;
        }

        private void ViewportMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            NavigateCamera(_sceneViewport.Navigation.MouseMove, e);
        }

        private void NavigateCamera(Action<int, int, Camera> navigateCameraAction, MouseEventArgs mouseEventArgs)
        {
            var position = mouseEventArgs.GetPosition(_sceneViewControl);
            navigateCameraAction((int)position.X, (int)position.Y, _sceneViewControl.Camera);
        }

        /// <summary>
        /// Disposes the viewport
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;
            UnBind();
            _disposed = true;
        }
    }
}