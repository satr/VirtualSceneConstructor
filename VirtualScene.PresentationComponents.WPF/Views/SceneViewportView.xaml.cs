using System.Windows.Input;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    ///     Interaction logic for SceneViewportView.xaml
    /// </summary>
    public partial class SceneViewportView
    {
        private readonly SceneViewportViewModel _sceneViewportViewModel;

        /// <summary>
        ///     Createts a new instance of the SceneViewportView user-control
        /// </summary>
        public SceneViewportView()
        {
            InitializeComponent();
            DataContext = _sceneViewportViewModel = new SceneViewportViewModel();
            MouseDown += OnMouseDownEventHandler;
            MouseUp += OnMouseUpEventHandler;
            MouseMove += OnMouseMoveEventHandler;
        }

        private void OnMouseMoveEventHandler(object s, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _sceneViewportViewModel.LeftButtonMouseMove(e.GetPosition(this));
            }
        }

        private void OnMouseUpEventHandler(object s, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                _sceneViewportViewModel.LeftButtonMouseUp(e.GetPosition(this));
            Mouse.OverrideCursor = null;
        }

        private void OnMouseDownEventHandler(object s, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            if (e.LeftButton == MouseButtonState.Pressed)
                _sceneViewportViewModel.LeftButtonMouseDown(e.GetPosition(this));
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _sceneViewportViewModel.NotifyViewTemplateApplied();
        }
    }
}
