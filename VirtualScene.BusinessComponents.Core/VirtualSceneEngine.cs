using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Version;
using VirtualScene.BusinessComponents.Core.Properties;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The 3D scene engine.
    /// </summary>
    public class VirtualSceneEngine
    {
        private const int DefaultUpdateRate = 40;//in milliseconds
        private const int MinimumWidth = 800;
        private const int MinimumHeight = 600;
        private DispatcherTimer _timer;

        /// <summary>
        /// The main OpenGL instance.
        /// </summary>
        private readonly OpenGL _gl = new OpenGL();
        private readonly Vertex _defaultCameraPosition;
        private readonly ObservableCollection<VirtualSceneViewport> _viewports = new ObservableCollection<VirtualSceneViewport>();

        /// <summary>
        /// Creates a new instance of the 3D scene.
        /// </summary>
        public VirtualSceneEngine()
        {
            CommonSceneContainer = new ObservableCollection<SceneElement>();
            CommonSceneContainer.CollectionChanged += CommonSceneContainerChanged;
            SetUpdateRate(DefaultUpdateRate);
            _defaultCameraPosition = new Vertex(-10, -10, 10);
            SetupTimer();
            CommonSceneContainer.Add(new Cube());
        }

        private void CommonSceneContainerChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                AddSceneElements(e.NewItems);
            }
            if (e.OldItems != null)
            {
                RemoveSceneElements(e.OldItems);
            }
        }

        private void RemoveSceneElements(IEnumerable sceneElements)
        {
            foreach (SceneElement sceneElement in sceneElements)
            {
                foreach (var viewport in _viewports)
                {
                    viewport.Scene.SceneContainer.RemoveChild(sceneElement);
                }
            }
        }

        private void AddSceneElements(IEnumerable sceneElements)
        {
            foreach (SceneElement sceneElement in sceneElements)
            {
                foreach (var viewport in _viewports)
                {
                    viewport.Scene.SceneContainer.AddChild(sceneElement);
                }
            }
        }

        /// <summary>
        /// The container with scene elements
        /// </summary>
        public ObservableCollection<SceneElement> CommonSceneContainer { get; set; }

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
        public VirtualSceneViewport CreateViewport()
        {
            var viewport = new VirtualSceneViewport(CreateScene());
            _viewports.Add(viewport);
            foreach (var sceneElement in CommonSceneContainer)
            {
                viewport.Scene.SceneContainer.AddChild(sceneElement);
            }
            return viewport;
        }

        private Scene CreateScene(int width = MinimumWidth, int height = MinimumHeight, int bitDepth = 32)
        {
            ValidateSceneArguments(width, height, bitDepth);
            _gl.Create(OpenGLVersion.OpenGL4_4, RenderContextType.DIBSection, width, height, bitDepth, null);
            var scene = new Scene { OpenGL = _gl };
            SharpGL.SceneGraph.Helpers.SceneHelper.InitialiseModelingScene(scene);
            scene.CurrentCamera = CreateCamera<ArcBallCamera>(_defaultCameraPosition);
            return scene;
        }

        private static void ValidateSceneArguments(int width, int height, int bitDepth)
        {
            if (width < MinimumWidth)
                throw new ArgumentOutOfRangeException("width", string.Format(Resources.Message_ValidateSceneArguments_Minimum_width_value_N, MinimumWidth));
            if (height < MinimumHeight)
                throw new ArgumentOutOfRangeException("height", string.Format(Resources.Message_ValidateSceneArguments_Minimum_height_value_N, MinimumHeight));
            if (bitDepth < 8)
                throw new ArgumentOutOfRangeException("bitDepth", Resources.Message_ValidateSceneArguments_Minimum_bitDepth_value_N);
            if (bitDepth > 32)
                throw new ArgumentOutOfRangeException("bitDepth", Resources.Message_ValidateSceneArguments_Maximum_bitDepth_value_N);
        }

        /// <summary>
        /// Creates the camera
        /// </summary>
        /// <typeparam name="T">Type of the camera.</typeparam>
        /// <param name="position">Position of the camera in the scene.</param>
        /// <returns>Returns a new camera.</returns>
        private static Camera CreateCamera<T>(Vertex position)
            where T:Camera, new()
        {
            return new T
            {
                Position = position
            };
        }
    }
}
