using System.Collections.ObjectModel;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Entities;
using VirtualScene.EntityPresentationComponents.WPF.Properties;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model for content view of the stage
    /// </summary>
    public class StageContentViewModel
    {
        private readonly ISceneContent _sceneContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="StageContentViewModel" />
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
            get { return _sceneContent.Stage.Items; }
        }
    }
}