using System;
using System.Collections.Generic;
using VirtualScene.BusinessComponents.Core.Controllers;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.Core.Managers
{
    /// <summary>
    /// The manager notifying subscrubers 
    /// </summary>
    public class CollectionNotificationControllerManager: OperationControllerManagerBase<ISubscriberSetController>
    {
        private readonly NullSubscriberSetController _nullSubscriberSetController = new NullSubscriberSetController();

        /// <summary>
        /// Pre-register operation-controllers.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<ISubscriberSetController> PreRegisterOperationControllers()
        {
            yield break; // subscribers are added by the method <see cref="Add" />
        }

        /// <summary>
        /// Build a null-operation-controller which is used when no operation-controller is found for specific type.
        /// </summary>
        /// <returns>The null-operation-controller.</returns>
        protected override ISubscriberSetController CreateNullOperationController()
        {
            return _nullSubscriberSetController;
        }

        /// <summary>
        /// Notify subscribers about an operation with the collection.
        /// </summary>
        /// <param name="dataType">The type of collection items.</param>
        /// <param name="data">Items on which the operation was performed..</param>
        public void Notify<T>(Type dataType, ICollection<T> data)
        {
            GetOperationControllerBy(dataType).Notify(data);
        }

        /// <summary>
        /// Add the new subscriber on operation with items in the collection.
        /// </summary>
        /// <typeparam name="T">The type of which subscriber is subscribed.</typeparam>
        /// <param name="subscriber">The subscriber to be notified.</param>
        public void Add<T>(ICollectionItemsOperationSubscriber subscriber)
        {
            var operationController = GetOperationControllerBy(typeof(T));
            if(operationController == _nullSubscriberSetController)
            {
                operationController = new SubscriberSetController<T>();
                Register(operationController);
            }
            operationController.Add(subscriber);
        }
    }
}