using System.Collections.Generic;
using System.Collections.ObjectModel;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.Repositories
{
    /// <summary>
    /// Operation with an archive as with a repository with hierarchy of entities.
    /// </summary>
    public class RepositoryEntity<T> : IRepositoryEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryEntity&lt;T&gt;" />.
        /// </summary>
        public RepositoryEntity(T entity)
        {
            Entity = entity;
            EntityType = new TypeInfo(entity);
            Items = new ObservableCollection<IRepositoryEntity>();
        }

        /// <summary>
        /// The entity of the 
        /// </summary>
        public T Entity { get; set; }

        /// <summary>   
        /// The type of the entity.
        /// </summary>
        public TypeInfo EntityType { get; private set; }

        /// <summary>
        /// Sub-entries of the respository entity.
        /// </summary>
        public IList<IRepositoryEntity> Items { get; private set; }
    }
}
