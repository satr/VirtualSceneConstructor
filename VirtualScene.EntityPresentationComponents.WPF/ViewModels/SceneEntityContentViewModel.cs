using System.Collections.Generic;
using System.Linq;
using VirtualScene.BusinessComponents.Core.Controllers;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.PresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model for the content view of a selected <see cref="ISceneEntity" />
    /// </summary>
    public class SceneEntityContentViewModel<TSceneEntity> : ViewModelBase, ICollectionItemsOperationSubscriber
        where TSceneEntity : class, ISceneEntity
    {
        private TSceneEntity _sceneEntity;

        /// <summary>
        /// Creates a new view-model of content-view of the scene-entity
        /// </summary>
        /// <param name="sceneContent">The content of the scene</param>
        public SceneEntityContentViewModel(ISceneContent sceneContent)
        {
            sceneContent.SubscribeOnSelectedItems<TSceneEntity>(this);
            Title = Resources.Title_Scene_Entity;
            TitleColumnWidth = 80;
        }

        /// <summary>
        /// The title of the view
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// The width of the column with property titles.
        /// </summary>
        public int TitleColumnWidth { get; set; }

        /// <summary>
        /// Notification about the operation with the collection.
        /// </summary>
        /// <param name="items">The changed collection.</param>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        public void Notify<T>(ICollection<T> items)
        {
            var sceneEntities = items as ICollection<ISceneEntity>;
            if (sceneEntities != null && sceneEntities.Count == 1)
                SceneEntity = sceneEntities.First() as TSceneEntity;
        }

        /// <summary>
        /// The <see cref="ISceneEntity" /> shown on the view.
        /// </summary>
        public TSceneEntity SceneEntity
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