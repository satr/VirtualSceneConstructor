using System.Windows.Input;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.Commands.SceneEntityCommands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model for content view of the stage
    /// </summary>
    public class StageContentViewModel: ViewModelBase
    {
        private IStage _stage;

        /// <summary>
        /// Initializes a new instance of the <see cref="StageContentViewModel" />
        /// </summary>
        /// <param name="sceneContent"></param>
        public StageContentViewModel(ISceneContent sceneContent)
        {
            SelectItemsCommand = new SelectSceneEntitiesCommand(sceneContent);
            sceneContent.StageChanged += (sender, stage) => Stage = stage;
            Stage = sceneContent.Stage;
        }

        /// <summary>
        /// The command processing the list of the selected <see cref="ISceneEntity" />
        /// </summary>
        public ICommand SelectItemsCommand { get; set; }

        /// <summary>
        /// The title of the content view
        /// </summary>
        public string Title
        {
            get { return Resources.Title_Stage; }
        }

        /// <summary>
        /// The stage in the scene-content.
        /// </summary>
        public IStage Stage
        {
            get { return _stage; }
            set
            {
                if (Equals(value, _stage)) 
                    return;
                _stage = value;
                OnPropertyChanged();
            }
        }
    }
}