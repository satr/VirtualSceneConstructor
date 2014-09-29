﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SharpGL.RenderContextProviders;
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

        private ImageSource _imageSource;

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
        public SceneViewport Viewport { get; set; }

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
            HandleMouseEvent(position, Viewport.Navigation.MouseDown);
        }

        /// <summary>
        /// Mouse up event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was up</param>
        public void MouseUp(Point position)
        {
            HandleMouseEvent(position, Viewport.Navigation.MouseUp);
        }

        /// <summary>
        /// Mouse move event occurs on the view
        /// </summary>
        /// <param name="position">Position where the mouse was moved</param>
        public void MouseMove(Point position)
        {
            HandleMouseEvent(position, Viewport.Navigation.MouseMove);
        }

        private static void HandleMouseEvent(Point position, Action<int, int> eventHandler)
        {
            eventHandler((int) position.X, (int) position.Y);
        }
    }
}