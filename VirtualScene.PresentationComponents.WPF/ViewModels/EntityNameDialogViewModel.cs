using System.Windows;
using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.Models;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view model for EntityNameDialogView
    /// </summary>
    public class EntityNameDialogViewModel
    {
        private readonly EntityNameViewModel _viewModel;
        private readonly Window _view;

        /// <summary>
        /// Creates a new instance of the EntityNameDialogViewModel
        /// </summary>
        /// <param name="title">The title of the dialog</param>
        /// <param name="view">The dialog view</param>
        /// <param name="viewModel">The entity name view model</param>
        public EntityNameDialogViewModel(string title, Window view, EntityNameViewModel viewModel)
        {
            Title = title;
            _viewModel = viewModel;
            _view = view;
            AcceptCommand = new DelegateCommand(()=>CloseView(true));
            CancelCommand = new DelegateCommand(()=>CloseView(false));
        }

        private void CloseView(bool accepted)
        {
            _view.DialogResult = accepted;
            _view.Close();
        }

        /// <summary>
        /// The command on the dialog accepted
        /// </summary>
        public DelegateCommand AcceptCommand { get; set; }

        /// <summary>
        /// The command on the dialog canceled
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// The title of the dialog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        public string Name
        {
            get { return _viewModel.Name; }
            set { _viewModel.Name = value; }
        }
    }
}