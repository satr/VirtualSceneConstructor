using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using VirtualScene.Common.Exceptions;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities
{
    /// <summary>
    /// The stage with objects in 3D environment
    /// </summary>
    public class Stage : IStage
    {
        private readonly ObservableCollection<ISceneEntity> _items = new ObservableCollection<ISceneEntity>();

        /// <summary>
        /// Occures when <see cref="ISceneEntity" /> is added to the stage.
        /// </summary>
        public event EventHandler<ISceneEntity> SceneEntityAdded;

        /// <summary>
        /// Occures when <see cref="ISceneEntity" /> is removed from the stage.
        /// </summary>
        public event EventHandler<ISceneEntity> SceneEntityRemoved;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stage" />
        /// </summary>
        public Stage()
        {
            Items = new ReadOnlyObservableCollection<ISceneEntity>(_items);
        }

        /// <summary>
        /// The name of the stage
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of visual representations of objects in the scene
        /// </summary>
        [XmlIgnore]
        public ReadOnlyObservableCollection<ISceneEntity> Items { get; private set; }

        public void Add(ISceneEntity sceneEntity)
        {
            if (_items.Contains(sceneEntity)) 
                throw new ItemAlreadyExistsException(Resources.Message_Scene_entity_already_exists_in_the_stage);
            _items.Add(sceneEntity);
            OnSceneEntityAdded(sceneEntity);
        }

        public void Remove(ISceneEntity sceneEntity)
        {
            if (!_items.Contains(sceneEntity)) 
                throw new ItemDoesNotExistException(Resources.Message_Scene_entity_does_not_exists_in_the_stage);
            _items.Remove(sceneEntity);
            OnSceneEntityRemoved(sceneEntity);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var stage = (Stage) obj;
            if (!string.Equals(stage.Name, Name) 
                || stage.Items.Count != Items.Count)
                return false;
// ReSharper disable LoopCanBeConvertedToQuery
            for (int i = 0; i < Items.Count; i++)
// ReSharper restore LoopCanBeConvertedToQuery
            {
                if (!stage.Items[i].Equals(Items[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Name == null? 0: Name.GetHashCode();
        }

        private void OnSceneEntityAdded(ISceneEntity sceneEntity)
        {
            var handler = SceneEntityAdded;
            if (handler != null) 
                handler(this, sceneEntity);
        }

        private void OnSceneEntityRemoved(ISceneEntity sceneEntity)
        {
            var handler = SceneEntityRemoved;
            if (handler != null) 
                handler(this, sceneEntity);
        }
    }
}