using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VirtualScene.PresentationComponents.WPF.Annotations;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// Base view model. 
    /// Supports <see cref="INotifyPropertyChanged"/> interface
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occures when the view should be closed.
        /// </summary>
        public event EventHandler CloseView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase" />
        /// </summary>
        protected ViewModelBase()
        {
            CancelCommand = new DelegateCommand(CancelOperation);
        }

        private void CancelOperation()
        {
            OperationCancelled = true;
            OnCloseView();
        }

        /// <summary>
        /// The command sets OperationCancelled property to true and closing the view.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged event invocator
        /// </summary>
        /// <param name="propertyName">The name of the changed property</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

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