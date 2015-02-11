using System;
using System.Windows.Input;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The base view-model for window views.
    /// Provides the event <see cref="WindowViewModelBase.CloseView"/>.
    /// </summary>
    public abstract class WindowViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Occures when the view should be closed.
        /// </summary>
        public event EventHandler CloseView;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowViewModelBase" />
        /// </summary>
        protected WindowViewModelBase()
        {
            CancelCommand = new DelegateCommand(CancelOperation);
        }

        private void CancelOperation()
        {
            OperationCancelled = true;
            OnCloseView();
        }

        /// <summary>
        /// The command sets <see cref="WindowViewModelBase.OperationCancelled" /> property to true and closing the view.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// The operation status. When  "true" - the operation has been cancelled
        /// </summary>
        public bool OperationCancelled { get; set; }

        /// <summary>
        /// Raise the CloseView event.
        /// </summary>
        protected void OnCloseView()
        {
            var handler = CloseView;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }    
    }
}