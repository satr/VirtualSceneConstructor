using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.WPF;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.PresentationComponents.WPF.Annotations;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// The view model for the SceneViewControl
    /// </summary>
    public class SceneViewModel: INotifyPropertyChanged
    {
        private const int MinSceneWidth = 20;

        private const int MinSceneHeight = 20;

        private int _lastSizeChangedWidth;

        private int _lastSizeChangedHeight;

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

        private ImageSource _imageSource;

        private double _frameRate = 28.0f;
        private SceneViewport _viewport;

        /// <summary>
        /// Creates a new instance of the SceneViewModel
        /// </summary>
        public SceneViewModel()
        {
            SceneResizeEnabled = true;
        }

        /// <summary>
        /// Handles the SizeChanged event of the SceneViewControl control.
        /// </summary>
        public void ChangeSceneViewSize(int width, int height)
        {
            //  If we don't have a scene, we're done.
            if (!SceneResizeEnabled || Scene == null)
                return;

            //  Lock on the scene.
            lock (Scene)
            {
                //  Get the dimensions.
                if (_lastSizeChangedHeight == height && _lastSizeChangedWidth == width)//avoid repeating resizing calls
                    return;
                if (width <= MinSceneWidth || height <= MinSceneHeight)//limit the minimum size of the scene.
                    return;
                _lastSizeChangedWidth = width;
                _lastSizeChangedHeight = height;
                //  Resize the scene.
                Scene.OpenGL.SetDimensions(width, height);
                Scene.Resize(width, height);
            }
        }

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        /// <value>
        /// The camera.
        /// </value>
        public Camera Camera { get; set; }

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
        /// Gets or sets a value indicating whether to draw FPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draw FPS; otherwise, <c>false</c>.
        /// </value>
        public bool DrawFPS { get; set; }


        /// <summary>
        /// Gets or sets the scene.
        /// </summary>
        /// <value>
        /// The scene.
        /// </value>
        public Scene Scene { get; set; }

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
            if (Scene == null)
                return;

            //  Lock on the Scene.
            lock (Scene)
            {
                //  Start the stopwatch so that we can time the rendering.
                _stopwatch.Restart();

                //  Draw the scene.
                Scene.Draw(Camera);

                //  Draw the FPS.
                if (DrawFPS)
                {
                    Scene.OpenGL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f, string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", _frameTime, 1000.0 / _frameTime));
                    Scene.OpenGL.Flush();
                }

                IntPtr hBitmap;
                if (TryGetHandleToBitmap(Scene.OpenGL.RenderContextProvider, out hBitmap))
                {
                    UpdateImageSource(hBitmap);
                }

                //  Stop the stopwatch.
                _stopwatch.Stop();

                //  Store the frame time.
                _frameTime = _stopwatch.Elapsed.TotalMilliseconds;
            }
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
            ImageSource = newFormatedBitmapSource;
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
        /// The viewport of the scene
        /// </summary>
        public SceneViewport Viewport
        {
            get { return _viewport; }
            set
            {
                if (Equals(value, _viewport)) 
                    return;
                _viewport = value;
                Scene = _viewport.Scene;
                Camera = Scene.CurrentCamera;
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

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged event invocator
        /// </summary>
        /// <param name="propertyName">The name of the changed property</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Mouse down event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was put down</param>
        public void MouseDown(Point position)
        {
            Viewport.Navigation.MouseDown((int)position.X, (int)position.Y, Camera);
        }

        /// <summary>
        /// Mouse up event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was up</param>
        public void MouseUp(Point position)
        {
            Viewport.Navigation.MouseUp((int)position.X, (int)position.Y, Camera);
        }

        /// <summary>
        /// Mouse move event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was moved</param>
        public void MouseMove(Point position)
        {
            Viewport.Navigation.MouseMove((int)position.X, (int)position.Y, Camera);
        }
    }
}