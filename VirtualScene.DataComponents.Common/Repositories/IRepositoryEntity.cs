using System.Collections.Generic;

namespace VirtualScene.DataComponents.Common.Repositories
{
    /// <summary>
    /// The entity of a repository.
    /// </summary>
    public interface IRepositoryEntity
    {
        /// <summary>
        /// Sub-entries of the respository entity.
        /// </summary>
        IList<IRepositoryEntity> Items { get; }
    }
}