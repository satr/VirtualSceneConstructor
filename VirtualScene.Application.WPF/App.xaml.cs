using System.Windows;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.Application.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// VirtualSceneContext holding a state of the virtual scene
        /// </summary>
        public SceneContent SceneContent { get; private set; }

        /// <summary>
        /// Raises the System.Windows.Application.Startup event.
        /// </summary>
        /// <param name="e">A System.Windows.StartupEventArgs that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SceneContent = new SceneContent();
        }
    }
}
