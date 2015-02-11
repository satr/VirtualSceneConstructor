using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualScene.Common
{
    /// <summary>
    /// The manager for operation-controllers performing operations for specific types.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OperationControllerManagerBase<T>
        where T: class, IOperationController
    {
        private IList<T> _registeredOperationControllers;
        private T _nullOperationController;

        /// <summary>
        /// Pre-register operation-controllers.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<T> PreRegisterOperationControllers();

        /// <summary>
        /// Find the operation-controller by the type of the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type used to find the operation-controller.</typeparam>
        /// <param name="entity">The entity which type is used to find the controller.</param>
        /// <returns>Returns the operation-controller registered for the <see cref="TEntity" />. 
        /// If corresponding operation-controller has not been registered - the <see cref="NullOperationController" /> is returned.</returns>
        protected T GetOperationControllerFor<TEntity>(TEntity entity)
        {
            if (entity == null)
                return NullOperationController;
            return RegisteredOperationControllers.FirstOrDefault(p => p.TypeKey.IsInstanceOfType(entity)) ?? NullOperationController;
        }

        /// <summary>
        /// Find the operation-controller by the type.
        /// </summary>
        /// <param name="type">The type used to find the operation-controller.</param>
        /// <returns>Returns the operation-controller registered for the <see cref="type" />. 
        /// If corresponding operation-controller has not been registered - the <see cref="NullOperationController" /> is returned.</returns>
        protected T GetOperationControllerBy(Type type)
        {
            if (type == null)
                return NullOperationController;
            return RegisteredOperationControllers.FirstOrDefault(p => p.TypeKey.IsAssignableFrom(type)) ?? NullOperationController;
        }

        /// <summary>
        /// Create a null-operation-controller which is used when no operation-controller is found for specific type.
        /// </summary>
        /// <returns>The null-operation-controller.</returns>
        protected abstract T CreateNullOperationController();

        /// <summary>
        /// Register the operation-controller.
        /// </summary>
        /// <param name="operationController">The type of the operation-controller.</param>
        protected void Register(T operationController)
        {
            RegisteredOperationControllers.Add(operationController);
        }

        private IList<T> RegisteredOperationControllers
        {
            get { return _registeredOperationControllers?? (_registeredOperationControllers = PreRegisterOperationControllers().ToList()); }
        }

        private T NullOperationController
        {
            get { return _nullOperationController ?? (_nullOperationController = CreateNullOperationController()); }
        }
    }
}