using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The 3D scene engine.
    /// </summary>
    public class SceneEngine
    {
        private DispatcherTimer _timer;


        private readonly ObservableCollection<SceneViewport> _viewports = new ObservableCollection<SceneViewport>();

        /// <summary>
        /// Creates a new instance of the 3D scene.
        /// </summary>
        public SceneEngine()
        {
            Cameras = new ObservableCollection<Camera>();
            UpdateRate = Constants.SceneEngine.DefaultUpdateRate;
            SetUpdateRate(Constants.SceneEngine.DefaultUpdateRate);
            Scene = new SceneFactory().Create();
            SetupTimer();
        }

        /// <summary>
        /// The scene
        /// </summary>
        public Scene Scene { get; private set; }

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
            var viewport = new SceneViewport(Scene, Cameras);
            _viewports.Add(viewport);
            return viewport;
        }

        /// <summary>
        /// Cameras in the scene
        /// </summary>
        public ObservableCollection<Camera> Cameras { get; private set; }
    }
}
