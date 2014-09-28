namespace VirtualScene.PresentationComponents.WPF
{
    /// <summary>
    ///     Interaction logic for SceneViewportView.xaml
    /// </summary>
    public partial class SceneViewportView
    {
        /// <summary>
        ///     Createts a new instance of the SceneViewportView user-control
        /// </summary>
        public SceneViewportView()
        {
            InitializeComponent();
            DataContext = new SceneViewportViewModel(SceneView);
        }
    }
}
