using System.Windows;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.PresentationComponents.WPF
{
    /// <summary>
    /// Interaction logic for SceneViewportView.xaml
    /// </summary>
    public partial class SceneViewportView
    {
        /// <summary>
        /// VirtualSceneContext property to create a viewport based on the SceneViewportView user-control
        /// </summary>
        public static readonly DependencyProperty SceneContentProperty = DependencyProperty.Register("SceneContent", typeof(SceneContent), typeof(SceneViewportView), new PropertyMetadata(null, UpdateVirtualSceneContent));
// ReSharper disable NotAccessedField.Local
        private Viewport _viewport;
// ReSharper restore NotAccessedField.Local

        private static void UpdateVirtualSceneContent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SceneViewportView) d).SceneContent = (SceneContent) e.NewValue;
        }

        /// <summary>
        /// Createts a new instance of the SceneViewportView user-control
        /// </summary>
        public SceneViewportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// VirtualSceneContext property to create a viewport based on the SceneViewportView user-control
        /// </summary>
        public SceneContent SceneContent
        {
            get { return (SceneContent)GetValue(SceneContentProperty); }
            set
            {
                var virtualSceneContent = value;
                SetValue(SceneContentProperty, virtualSceneContent);
                _viewport = new Viewport(SceneView, virtualSceneContent);
            }
        }
    }
}
