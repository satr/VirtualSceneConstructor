using VirtualScene.Common;

namespace VirtualScene.DataComponents.Common.DataAdapters
{
    /// <summary>
    /// The data adapter to persist data
    /// </summary>
    public interface IDataAdapter<T>
    {
        /// <summary>
        /// Save an entity
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        /// <returns>Result of the operation</returns>
        IActionResult Save(T entity);

        /// <summary>
        /// Load the entity by its name.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <returns>The result of the operation with loaded entity in action-result property Value.</returns>
        ActionResult<T> Load(string name);
    }
}