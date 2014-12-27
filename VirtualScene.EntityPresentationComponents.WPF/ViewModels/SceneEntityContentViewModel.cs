using System.Collections.Generic;
using System.Linq;
using VirtualScene.BusinessComponents.Core.Controllers;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Entities;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model for the content view of a selected scene-entity
    /// </summary>
    public class SceneEntityContentViewModel : ViewModelBase, ICollectionItemsOperationSubscriber
    {
        private ISceneEntity _sceneEntity;

        /// <summary>
        /// Creates a new view-model of content-view of the scene-entity
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public SceneEntityContentViewModel(ISceneContent sceneContent)
        {
            sceneContent.SubscribeOnSelectedItems<ISceneEntity>(this);
        }

        /// <summary>
        /// Notification about the operation with the collection.
        /// </summary>
        /// <param name="items">The changed collection.</param>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        public void Notify<T>(ICollection<T> items)
        {
            var sceneEntities = items as ICollection<ISceneEntity>;
            if (sceneEntities != null && sceneEntities.Count == 1)
                SceneEntity = sceneEntities.First();
        }

        /// <summary>
        /// The <see cref="ISceneEntity" /> shown on the view.
        /// </summary>
        public ISceneEntity SceneEntity
        {
            get { return _sceneEntity; }
            set
            {
                if (Equals(value, _sceneEntity) || value == null) 
                    return;
                _sceneEntity = value;
                OnPropertyChanged();
            }
        }
    }
}