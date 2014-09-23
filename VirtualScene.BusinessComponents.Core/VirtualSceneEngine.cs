using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The 3D scene engine.
    /// </summary>
    public class VirtualSceneEngine
    {
        private DispatcherTimer _timer;


        private readonly ObservableCollection<VirtualSceneViewport> _viewports = new ObservableCollection<VirtualSceneViewport>();
        private readonly VirtualSceneFactory _virtualSceneFactory;

        /// <summary>
        /// Creates a new instance of the 3D scene.
        /// </summary>
        public VirtualSceneEngine()
        {
            CommonSceneContainer = new ObservableCollection<SceneElement>();
            CommonSceneContainer.CollectionChanged += CommonSceneContainerChanged;
            SetUpdateRate(Constants.VirtualScene.DefaultUpdateRate);
            SetupTimer();
            _virtualSceneFactory = new VirtualSceneFactory();
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
            var viewport = new VirtualSceneViewport(_virtualSceneFactory.Create());
            _viewports.Add(viewport);
            foreach (var sceneElement in CommonSceneContainer)
            {
                viewport.Scene.SceneContainer.AddChild(sceneElement);
            }
            return viewport;
        }
    }
}
