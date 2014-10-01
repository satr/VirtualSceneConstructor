using System.ComponentModel;
using System.Runtime.CompilerServices;
using VirtualScene.PresentationComponents.WPF.Annotations;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// Base view model. 
    /// Supports <see cref="INotifyPropertyChanged"/> interface
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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
    }
}