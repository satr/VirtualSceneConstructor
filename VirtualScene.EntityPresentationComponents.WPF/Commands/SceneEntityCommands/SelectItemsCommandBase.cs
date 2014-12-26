using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.EntityPresentationComponents.WPF.Commands.SceneEntityCommands
{
    /// <summary>
    /// The command processing a collection of items as a parameter.
    /// </summary>
    /// <typeparam name="T">The type of items.</typeparam>
    public abstract class SelectItemsCommandBase<T> : CommandBase 
        where T : class
    {
        /// <summary>
        /// Process the collection of items.
        /// </summary>
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
        {
            var selectedItems = parameter as IEnumerable;
            if(selectedItems == null)
                return;
            ProcessItems(selectedItems.OfType<T>().ToList());
        }

        /// <summary>
        /// Process the collection of items passed as a parameter of execution.
        /// </summary>
        /// <param name="items">The collection of items</param>
        protected abstract void ProcessItems(IEnumerable<T> items);
    }
}