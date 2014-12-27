using System;
using System.Collections.Generic;

namespace VirtualScene.BusinessComponents.Core.Controllers
{
    /// <summary>
    /// The set of subscribers on <see cref="Notify{T}" />  notification.
    /// </summary>
    public class SubscriberSetController<TKey> : ISubscriberSetController
    {
        private readonly List<ICollectionItemsOperationSubscriber> _subscribers = new List<ICollectionItemsOperationSubscriber>();

        /// <summary>
        /// The type-key defining the operation controller.
        /// </summary>
        public Type TypeKey
        {
            get { return typeof(TKey); }
        }

        /// <summary>
        /// Add a new subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber to be notified.</param>
        public void Add(ICollectionItemsOperationSubscriber subscriber)
        {
            if (!_subscribers.Contains(subscriber))
                _subscribers.Add(subscriber);
        }

        /// <summary>
        /// Notify subscribers.
        /// </summary>
        /// <param name="items">The notification items.</param>
        /// <typeparam name="T">The type of items.</typeparam>
        public void Notify<T>(ICollection<T> items)
        {
            foreach (var subscriber in _subscribers)
                subscriber.Notify(items);
        }
    }
}