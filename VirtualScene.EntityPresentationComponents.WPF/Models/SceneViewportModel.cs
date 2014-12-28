using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph.Cameras;
using SharpGL.WPF;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.EntityPresentationComponents.WPF.Commands;
using VirtualScene.EntityPresentationComponents.WPF.Commands.CameraCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.Models;

namespace VirtualScene.EntityPresentationComponents.WPF.Models
{
    /// <summary>
    /// The model of the scene viewport
    /// </summary>
    public class SceneViewportModel 
    {
        /// <summary>
        /// The dispatcher timer.
        /// </summary>
        private DispatcherTimer _timer;

        /// <summary>
        /// A stopwatch used for timing rendering.
        /// </summary>
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// The last frame time in milliseconds.
        /// </summary>
        private double _frameTime;
        private double _frameRate = 28.0f;

        /// <summary>
        /// Build ethe new instance of the viewport
        /// </summary>
        /// <param name="sceneContent"></param>
        public SceneViewportModel(ISceneContent sceneContent)
        {
            SceneResizeEnabled = false;
            var sceneEngine = sceneContent.SceneEngine;
            Viewport = sceneEngine.CreateViewport();
            Viewport.FPSEnabled = true;
            InitViewportContextMenu(sceneEngine.Cameras);
        }

        /// <summary>
        /// Navigation within the viewport
        /// </summary>
        public SceneViewportNavigation Navigation
        {
            get { return Viewport.Navigation; }
        }

        private SceneViewport Viewport { get; set; }

        private void InitViewportContextMenu(ObservableCollection<Camera> cameras)
        {
            ViewportContextMenu = new ObservableCollection<MenuItem>();
            foreach (var camera in cameras)
                ViewportContextMenu.Add(CreateViewportMenuItemSelectCamera(camera));
            cameras.CollectionChanged += CamerasCollectionChanged;
        }

        private void CamerasCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AddCameras(e.NewItems);
            RemoveCameras(e.OldItems);
        }

        private void AddCameras(IEnumerable addedCameras)
        {
            if (addedCameras == null) 
                return;
            foreach (Camera addedCamera in addedCameras)
            {
                ViewportContextMenu.Add(CreateViewportMenuItemSelectCamera(addedCamera));
            }
        }

        private void RemoveCameras(IEnumerable removedCameras)
        {
            if (removedCameras == null) 
                return;
            foreach (Camera removedCamera in removedCameras)
            {
                var menuItem = ViewportContextMenu.FirstOrDefault(mi => Equals(mi.Tag, removedCamera));
                if (menuItem == null)
                    continue;
                ViewportContextMenu.Remove(menuItem);
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
                Command = new SetCameraToSceneViewCommand(Viewport, camera),
            };
            return menuItem;
        }


        /// <summary>
        /// Handles the SizeChanged event of the SceneViewControl control.
        /// </summary>
        public void ChangeSceneViewSize(int width, int height)
        {
            //  If we don't have a scene, we're done.
            if (!SceneResizeEnabled || Viewport == null || Viewport.Scene == null)
                return;

            //  Lock on the scene.
            lock (Viewport.Scene)
            {
                Viewport.ResizeScene(width, height);
            }
        }

        /// <summary>
        /// Enables resizing of the scene. 
        /// Resizing should be turned off when several viewports may be available in the application for same scene.
        /// </summary>
        public bool SceneResizeEnabled { get; set; }


        /// <summary>
        /// Gets or sets the frame rate.
        /// </summary>
        /// <value>The frame rate.</value>
        public double FrameRate
        {
            get { return _frameRate; }
            set
            {
                if (value.Equals(_frameRate))
                    return;
                _frameRate = value;
                SetupTimer(_frameRate);
            }
        }

        /// <summary>
        /// Setup update scene DispatcherTimer
        /// </summary>
        public void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            SetupTimer(FrameRate);
        }

        /// <summary>
        /// Called when the frame rate is changed.
        /// </summary>
        /// <param name="frameRate">The update frame interval</param>
        private void SetupTimer(double frameRate)
        {
            _timer.Stop();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000f / frameRate));
            _timer.Start();
        }


        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void TimerTick(object sender, EventArgs e)
        {
            //  If we don't have a scene, we're done.
            if (Viewport == null || Viewport.Scene == null)
                return;

            //  Lock on the Scene.
            lock (Viewport.Scene)
            {
                //  Start the stopwatch so that we can time the rendering.
                _stopwatch.Restart();

                Viewport.DrawScene();
                Viewport.DrawFPS(_frameTime);

                IntPtr hBitmap;
                if (TryGetHandleToBitmap(Viewport.Scene.OpenGL.RenderContextProvider, out hBitmap))
                {
                    UpdateImageSource(hBitmap);
                }

                //  Stop the stopwatch.
                _stopwatch.Stop();

                //  Store the frame time.
                _frameTime = _stopwatch.Elapsed.TotalMilliseconds;
            }
        }

        private static bool TryGetHandleToBitmap(IRenderContextProvider renderContextProvider, out IntPtr handleToBitmap)
        {
            var dibSectionRenderContextProvider = renderContextProvider as DIBSectionRenderContextProvider;
            if (dibSectionRenderContextProvider != null)
            {
                handleToBitmap = dibSectionRenderContextProvider.DIBSection.HBitmap;
                return true;
            }
            var fboRenderContextProvider = renderContextProvider as FBORenderContextProvider;
            if (fboRenderContextProvider != null)
            {
                handleToBitmap = fboRenderContextProvider.InternalDIBSection.HBitmap;
                return true;
            }
            handleToBitmap = IntPtr.Zero;
            return false;
        }


        private void UpdateImageSource(IntPtr hBitmap)
        {
            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
            //  meaning the drawing comes out transparent.
            var newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(hBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            //  Copy the pixels over.
            OnImageSourceUpdated(new ImageSourceEventArg(newFormatedBitmapSource));
        }

        /// <summary>
        /// Occured when an image is updated
        /// </summary>
        public event EventHandler<ImageSourceEventArg> ImageSourceUpdated;

        /// <summary>
        /// Event invocator for updated image source
        /// </summary>
        /// <param name="e">Event argument for an updated image</param>
        private void OnImageSourceUpdated(ImageSourceEventArg e)
        {
            var handler = ImageSourceUpdated;
            if (handler != null) 
                handler(this, e);
        }
    }
}