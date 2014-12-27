using System.Collections.Generic;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.Core.Controllers
{
    /// <summary>
    /// The set of subscribers on <see cref="Notify{T}" />  notification.
    /// </summary>
    public interface ISubscriberSetController : IOperationController
    {
        /// <summary>
        /// Add a new subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber to be notified.</param>
        void Add(ICollectionItemsOperationSubscriber subscriber);

        /// <summary>
        /// Notify subscribers.
        /// </summary>
        /// <param name="items">The notification items.</param>
        /// <typeparam name="T">The type of items.</typeparam>
        void Notify<T>(ICollection<T> items);
    }
}