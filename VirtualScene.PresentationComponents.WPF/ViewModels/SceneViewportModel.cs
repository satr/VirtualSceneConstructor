using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
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
        private readonly SceneViewport _sceneViewport;

        /// <summary>
        /// Create ethe new instance of the viewport
        /// </summary>
        /// <param name="sceneViewModel"></param>
        /// <param name="sceneContent"></param>
        public SceneViewportModel(SceneViewModel sceneViewModel, SceneContent sceneContent)
        {
            _sceneViewport = sceneContent.SceneEngine.CreateViewport();
            sceneViewModel.Viewport = _sceneViewport;
            sceneViewModel.Viewport.FPSEnabled = true;
            sceneViewModel.SceneResizeEnabled = false;
            InitViewportContextMenu(sceneContent);
            sceneContent.SceneEngine.Cameras.CollectionChanged += CamerasCollectionChanged;
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
                Command = new SetCameraToSceneViewCommand(_sceneViewport, camera),
            };
            return menuItem;
        }
    }
}