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
        private readonly VirtualSceneViewport _virtualSceneViewport;

        /// <summary>
        /// Create ethe new instance of the viewport
        /// </summary>
        /// <param name="sceneView"></param>
        /// <param name="virtualSceneContent"></param>
        public Viewport(SceneView sceneView, VirtualSceneContent virtualSceneContent)
        {
            _sceneView = sceneView;
            _virtualSceneViewport = virtualSceneContent.VirtualSceneEngine.CreateViewport();
            _sceneView.Scene = _virtualSceneViewport.Scene;
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
            _virtualSceneViewport.Navigation.MouseDown((int) position.X, (int) position.Y);
        }

        private void ViewportMouseUp(object sender, MouseButtonEventArgs e)
        {
            var sceneView = ((SceneView)sender);
            var position = e.GetPosition(sceneView);
            _virtualSceneViewport.Navigation.MouseUp((int) position.X, (int) position.Y);
            Mouse.OverrideCursor = null;
        }

        private void ViewportMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            var position = e.GetPosition((UIElement)sender);
            _virtualSceneViewport.Navigation.MouseMove((int)position.X, (int)position.Y);
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