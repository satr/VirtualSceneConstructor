using System;
using System.Collections.Generic;

namespace VirtualScene.BusinessComponents.Core.Controllers
{
    /// <summary>
    /// The null-set of subscribers on <see cref="Notify{T}" />  notification. This is used when specific type is not registered yet.
    /// </summary>
    public class NullSubscriberSetController : ISubscriberSetController
    {
        /// <summary>
        /// The type-key defining the operation controller.
        /// </summary>
        public Type TypeKey
        {
            get { return typeof (object); }
        }

        /// <summary>
        /// Add a new subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber to be notified.</param>
        public void Add(ICollectionItemsOperationSubscriber subscriber)
        {
        }

        /// <summary>
        /// Notify subscribers.
        /// </summary>
        /// <param name="items">The notification items.</param>
        /// <typeparam name="T">The type of items.</typeparam>
        public void Notify<T>(ICollection<T> items)
        {
        }
    }
}