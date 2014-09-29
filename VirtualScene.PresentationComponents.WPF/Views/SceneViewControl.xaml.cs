using System.Windows.Input;

namespace VirtualScene.PresentationComponents.WPF.Views
{
    /// <summary>
    /// Interaction logic for SceneViewControl.xaml
    /// </summary>
    public partial class SceneViewControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneViewControl"/> class.
        /// </summary>
        public SceneViewControl()
        {
            InitializeComponent();
            DataContext = ViewModel = new SceneViewModel();
            MouseDown += OnMouseDownEventHandler;
            MouseUp += OnMouseUpEventHandler;
            MouseMove += OnMouseMoveEventHandler;
        }

        private void OnMouseMoveEventHandler(object s, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) 
                return;
            ViewModel.MouseMove(e.GetPosition(this));
        }

        private void OnMouseUpEventHandler(object s, MouseButtonEventArgs e)
        {
            ViewModel.MouseUp(e.GetPosition(this));
            Mouse.OverrideCursor = null;
        }

        private void OnMouseDownEventHandler(object s, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            ViewModel.MouseDown(e.GetPosition(this));
        }

        /// <summary>
        /// The view model of the SceneViewControl
        /// </summary>
        public SceneViewModel ViewModel { get; private set; }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ViewModel.SetupTimer();
        }
    }
}
