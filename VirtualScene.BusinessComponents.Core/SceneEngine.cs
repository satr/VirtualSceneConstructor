using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.BusinessComponents.Core.Properties;
using VirtualScene.Common;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The 3D scene engine.
    /// </summary>
    public class SceneEngine : ISceneEngine
    {
        private DispatcherTimer _timer;

        private readonly ObservableCollection<SceneViewport> _viewports = new ObservableCollection<SceneViewport>();

        /// <summary>
        /// Creates a new instance of the 3D scene.
        /// </summary>
        public SceneEngine()
        {
            UpdateRate = Constants.SceneEngine.DefaultUpdateRate;
            SetUpdateRate(Constants.SceneEngine.DefaultUpdateRate);
            Scene = ServiceLocator.Get<SceneFactory>().Create();
            var defaultCamera = ServiceLocator.Get<CameraFactory>().Create<LookAtCamera>(Constants.SceneEngine.DefaultCameraVector, Resources.Title_Navigation_Camera);
            Cameras = new ObservableCollection<Camera>
            {
                defaultCamera,
            };
            Scene.CurrentCamera = defaultCamera;
            SetupTimer();
        }

        /// <summary>
        /// The scene
        /// </summary>
        private Scene Scene { get; set; }

        /// <summary>
        /// The update rate for external modifications.
        /// </summary>
        public int UpdateRate { get; private set; }

        /// <summary>
        /// Set the update rate for external modifications.
        /// </summary>
        /// <param name="value">the update rate (in milliseconds). Minimum: 1 ms</param>
        /// <returns></returns>
        public bool SetUpdateRate(int value)
        {
            if (value < 1)
                return false;
            UpdateRate = value;
            SetupTimer();
            return true;
        }

        private void SetupTimer()
        {
             DisposeTimer();
            _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, UpdateRate);
            _timer.Start();
        }

        private void DisposeTimer()
        {
            if (_timer == null)
                return;
            _timer.Tick -= TimerTick;
            if(_timer.IsEnabled)
                _timer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Creates the viewport with a scene. All previously added acene elements are automatically added to the new vieport.
        /// </summary>
        /// <returns>Returnd the new viewport</returns>
        public SceneViewport CreateViewport()
        {
            var viewport = new SceneViewport(Scene);
            _viewports.Add(viewport);
            return viewport;
        }

        /// <summary>
        /// Cameras in the scene
        /// </summary>
        public ObservableCollection<Camera> Cameras { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        public void Move(float dx, float dy, float dz)
        {
            foreach (var viewport in _viewports)
            {
                viewport.Navigation.Move(dx, dy, dz);
            }
        }

        /// <summary>
        /// Add an entity to the scene
        /// </summary>
        /// <param name="sceneEntity">The entity to be added to the scene</param>
        public void AddSceneEntity(ISceneEntity sceneEntity)
        {
            var sceneElement = sceneEntity.Geometry;
            if (!Scene.SceneContainer.Children.Contains(sceneElement))
                Scene.SceneContainer.AddChild(sceneElement);
        }

        /// <summary>
        /// Remove an entity from the scene
        /// </summary>
        /// <param name="sceneEntity">The entity to be removed from the scene</param>
        public void RemoveSceneEntity(ISceneEntity sceneEntity)
        {
            var sceneElement = sceneEntity.Geometry;
            if (Scene.SceneContainer.Children.Contains(sceneElement))
                Scene.SceneContainer.RemoveChild(sceneElement);
        }

        /// <summary>
        /// Clear the scene
        /// </summary>
        public void Clear()
        {
            var sceneElements = Scene.SceneContainer.Traverse();
            foreach (var sceneElement in sceneElements.Where(sceneElement => sceneElement is Polygon || sceneElement is Quadric).ToList())
                sceneElement.Parent.RemoveChild(sceneElement);
        }

        /// <summary>
        /// Count of SceneElements in the scene
        /// </summary>
        public int SceneElementsCount
        {
            get { return Scene.SceneContainer.Children.Count; }
        }

        /// <summary>
        /// SceneElements of the scene
        /// </summary>
        public ObservableCollection<SceneElement> SceneElements {
            get { return Scene.SceneContainer.Children; }
        }

        /// <summary>
        /// Gets the asset list of epecified type
        /// </summary>
        /// <typeparam name="T">Type of the asset</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAssets<T>() where T : Asset
        {
            return Scene.Assets.OfType<T>();
        }

        /// <summary>
        /// Add a new asset to the scene
        /// </summary>
        /// <param name="asset"></param>
        public void AddAsset<T>(T asset) where T : Asset
        {
            Scene.Assets.Add(asset);
        }

        /// <summary>
        /// Load existing or create a new texture.
        /// </summary>
        /// <param name="path">Path to the file of texture</param>
        /// <param name="textureFile">File name of texture</param>
        /// <returns>The texture</returns>
        public Texture LoadOrCreateTexture(string path, string textureFile)
        {
            var texture = GetAssets<Texture>().FirstOrDefault(t => Equals(t.Name, textureFile));
            if (texture != null)
                return texture;

            texture = new Texture();
            if (!File.Exists(textureFile))
            {
                var directoryName = Path.GetDirectoryName(path);
                var fileName = Path.GetFileName(textureFile);
                if (string.IsNullOrWhiteSpace(directoryName) || string.IsNullOrWhiteSpace(fileName))
                    return texture;
                textureFile = Path.Combine(directoryName, fileName);
                if (!File.Exists(textureFile))
                    return texture;
            }

            texture.Create(Scene.OpenGL, textureFile);
            return texture;
        }
    }
}
