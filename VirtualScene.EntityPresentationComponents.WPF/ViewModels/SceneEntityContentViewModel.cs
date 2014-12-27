using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Events;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model for the content view of a selected scene-entity
    /// </summary>
    public class SceneEntityContentViewModel : ICollectionChangedSubscriber
    {
        /// <summary>
        /// Creates a new view-model of content-view of the scene-entity
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public SceneEntityContentViewModel(ISceneContent sceneContent)
        {
            sceneContent.SubscribeOnSelectedItems<ISceneContent>(this);
        }
    }
}