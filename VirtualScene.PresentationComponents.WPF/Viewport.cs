using System.Windows;
using System.Windows.Input;
using SharpGL.WPF;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF
{
    /// <summary>
    /// THe viewport in WPF presentation component
    /// </summary>
    public class Viewport
    {
        private bool _disposed;
        private readonly SceneView _sceneView;
        private readonly SceneViewport _sceneViewport;

        /// <summary>
        /// Create ethe new instance of the viewport
        /// </summary>
        /// <param name="sceneView"></param>
        /// <param name="sceneContent"></param>
        public Viewport(SceneView sceneView, SceneContent sceneContent)
        {
            _sceneView = sceneView;
            _sceneViewport = sceneContent.SceneEngine.CreateViewport();
            _sceneView.Scene = _sceneViewport.Scene;
            Bind();
        }

        private void Bind()
        {
            if (_sceneView == null)
                return;
            _sceneView.MouseDown += ViewportMouseDown;
            _sceneView.MouseUp += ViewportMouseUp;
            _sceneView.MouseMove += ViewportMouseMove;
            _sceneView.KeyDown += ViewportKeyDown;
        }

        private void UnBind()
        {
            if(_sceneView == null)
                return;
            _sceneView.MouseDown -= ViewportMouseDown;
            _sceneView.MouseUp -= ViewportMouseUp;
            _sceneView.MouseMove -= ViewportMouseMove;
            _sceneView.KeyDown -= ViewportKeyDown;
        }

        private static void ViewportKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ViewportMouseDown(object sender, MouseButtonEventArgs e)
        {
            var sceneView = ((SceneView)sender);
            Mouse.OverrideCursor = Cursors.None;
            var position = e.GetPosition(sceneView);
            _sceneViewport.Navigation.MouseDown((int) position.X, (int) position.Y);
        }

        private void ViewportMouseUp(object sender, MouseButtonEventArgs e)
        {
            var sceneView = ((SceneView)sender);
            var position = e.GetPosition(sceneView);
            _sceneViewport.Navigation.MouseUp((int) position.X, (int) position.Y);
            Mouse.OverrideCursor = null;
        }

        private void ViewportMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            var position = e.GetPosition((UIElement)sender);
            _sceneViewport.Navigation.MouseMove((int)position.X, (int)position.Y);
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