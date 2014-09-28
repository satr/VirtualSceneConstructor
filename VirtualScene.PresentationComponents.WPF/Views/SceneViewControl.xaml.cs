using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SharpGL.RenderContextProviders;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.WPF;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SceneViewControl.xaml
    /// </summary>
    public partial class SceneViewControl
    {
        private const int MinSceneWidth = 20;
        private const int MinSceneHeight = 20;

        /// <summary>
        /// The camera dependency property.
        /// </summary>
        private static readonly DependencyProperty CameraProperty = DependencyProperty.Register("Camera", typeof(Camera), typeof(SceneViewControl), new PropertyMetadata(null, OnCameraChanged));

        /// <summary>
        /// The DrawFPS property.
        /// </summary>
        private static readonly DependencyProperty DrawFPSProperty = DependencyProperty.Register("DrawFPS", typeof(bool), typeof(SceneViewControl), new PropertyMetadata(false, null));

        /// <summary>
        /// The Scene Dependency Property.
        /// </summary>
        private static readonly DependencyProperty SceneProperty = DependencyProperty.Register("Scene", typeof(Scene), typeof(SceneViewControl), new PropertyMetadata(null, OnSceneChanged));

        /// <summary>
        /// The SceneResizeEnabled Dependency Property.
        /// </summary>
        private static readonly DependencyProperty SceneResizeEnabledProperty = DependencyProperty.Register("SceneResizeEnabled", typeof(bool), typeof(SceneViewControl), new PropertyMetadata(true, null));

        private int _lastSizeChangedWidth;
        private int _lastSizeChangedHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneViewControl"/> class.
        /// </summary>
        public SceneViewControl()
        {
            InitializeComponent();

            //  Handle the size changed event.
            SizeChanged += SceneViewSizeChanged;
        }

        /// <summary>
        /// Handles the SizeChanged event of the SceneViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        void SceneViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //  If we don't have a scene, we're done.
            if (!SceneResizeEnabled || Scene == null)
                return;

            //  Lock on the scene.
            lock (Scene)
            {
                //  Get the dimensions.
                var width = (int)e.NewSize.Width;
                var height = (int)e.NewSize.Height;
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
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            //  DispatcherTimer setup
            _timer = new DispatcherTimer();
            _timer.Tick += timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000.0 / FrameRate));
            _timer.Start();
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, EventArgs e)
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
                if(TryGetHandleToBitmap(Scene.OpenGL.RenderContextProvider, out hBitmap))
                {
                    UpdateImageSource(hBitmap, RenderingImage);
                }

                //  Stop the stopwatch.
                _stopwatch.Stop();
                
                //  Store the frame time.
                _frameTime = _stopwatch.Elapsed.TotalMilliseconds;
            }
        }

        private static void UpdateImageSource(IntPtr hBitmap, Image renderingImage)
        {
            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
            //  meaning the drawing comes out transparent.
            var newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(hBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            //  Copy the pixels over.
            renderingImage.Source = newFormatedBitmapSource;
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
        /// Called when the frame rate is changed.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnFrameRateChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Get the control.
            var me = o as SceneViewControl;

            //  Stop the timer.
            if (me == null) 
                return;
            
            me._timer.Stop();
            //  Set the timer.
            me._timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000f / me.FrameRate));
            //  Start the timer.
            me._timer.Start();
        }

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

        /// <summary>
        /// The frame rate dependency property.
        /// </summary>
        private static readonly DependencyProperty FrameRateProperty =
          DependencyProperty.Register("FrameRate", typeof(double), typeof(SceneViewControl),
          new PropertyMetadata(28.0, OnFrameRateChanged));

        /// <summary>
        /// Gets or sets the frame rate.
        /// </summary>
        /// <value>The frame rate.</value>
        public double FrameRate
        {
            get { return (double)GetValue(FrameRateProperty); }
            set { SetValue(FrameRateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw FPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draw FPS; otherwise, <c>false</c>.
        /// </value>
        public bool DrawFPS
        {
            get { return (bool)GetValue(DrawFPSProperty); }
            set { SetValue(DrawFPSProperty, value); }
        }


        /// <summary>
        /// Gets or sets the scene.
        /// </summary>
        /// <value>
        /// The scene.
        /// </value>
        public Scene Scene
        {
            get { return (Scene)GetValue(SceneProperty); }
            set { SetValue(SceneProperty, value); }
        }

        /// <summary>
        /// Called when [scene changed].
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSceneChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            //  Get the scene view.
//            var me = o as SceneViewControl;
        }

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        /// <value>
        /// The camera.
        /// </value>
        public Camera Camera
        {
            get { return (Camera)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        /// <summary>
        /// Enables resizing of the scene. 
        /// Resizing should be turned off when several viewports may be available in the application for same scene.
        /// </summary>
        public bool SceneResizeEnabled
        {
            get { return (bool)GetValue(SceneResizeEnabledProperty); }
            set { SetValue(SceneResizeEnabledProperty, value); }
        }

        /// <summary>
        /// Called when [camera changed].
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCameraChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
//            var me = o as SceneViewControl;
        }
    }

}
