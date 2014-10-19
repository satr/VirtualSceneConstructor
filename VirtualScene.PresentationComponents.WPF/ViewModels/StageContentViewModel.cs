using System.Collections.ObjectModel;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.PresentationComponents.WPF.Properties;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model for content view of the stage
    /// </summary>
    public class StageContentViewModel
    {
        private readonly ISceneContent _sceneContent;

        /// <summary>
        /// Creates a new instance of the StageContentViewModel
        /// </summary>
        /// <param name="sceneContent"></param>
        public StageContentViewModel(ISceneContent sceneContent)
        {
            _sceneContent = sceneContent;
        }

        /// <summary>
        /// The title of the content view
        /// </summary>
        public string Title
        {
            get { return Resources.Title_Stage; }
        }

        /// <summary>
        /// The content of the view
        /// </summary>
        public ObservableCollection<ISceneEntity> Source {
            get { return _sceneContent.Stage.Entities; }
        }
    }
}