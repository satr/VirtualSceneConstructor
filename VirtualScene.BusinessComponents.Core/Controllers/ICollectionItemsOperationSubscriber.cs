using System.Collections.Generic;

namespace VirtualScene.BusinessComponents.Core.Controllers
{
    /// <summary>
    /// The subscriber on operations with items of the collection.
    /// </summary>
    public interface ICollectionItemsOperationSubscriber
    {
        /// <summary>
        /// Notification about the operations with items of the collection.
        /// </summary>
        /// <param name="items">The operated items of the collection.</param>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        void Notify<T>(ICollection<T> items);
    }
}