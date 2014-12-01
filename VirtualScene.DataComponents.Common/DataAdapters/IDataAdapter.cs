using VirtualScene.Common;

namespace VirtualScene.DataComponents.Common.DataAdapters
{
    /// <summary>
    /// The data adapter to persist data
    /// </summary>
    public interface IDataAdapter<in T>
    {
        /// <summary>
        /// Save an entity
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        /// <returns>Result of the operation</returns>
        IActionResult Save(T entity);
    }
}