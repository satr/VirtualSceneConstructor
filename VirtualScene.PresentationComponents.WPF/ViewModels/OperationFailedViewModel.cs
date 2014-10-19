﻿using System.Collections.Generic;
using System.Windows.Input;
using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.Properties;

namespace VirtualScene.PresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view-model of the OperationFailedView
    /// </summary>
    public class OperationFailedViewModel: ViewModelBase
    {
        private readonly string _operationName;

        /// <summary>
        /// Creates a new instance of the view-model 
        /// </summary>
        /// <param name="operationName">The name of failed operation</param>
        /// <param name="errors"></param>
        public OperationFailedViewModel(string operationName, params string[] errors)
        {
            Errors = errors;
            _operationName = operationName;
            RepeatCommand = new DelegateCommand(OnCloseView);
        }

        /// <summary>
        /// The command closes the view without setting OperationCancelled - to repeat the operation.
        /// </summary>
        public ICommand RepeatCommand { get; set; }

        /// <summary>
        /// The title of the view
        /// </summary>
        public string Title
        {
            get { return string.Format(Resources.Title_Operation_failed_N, _operationName); }
        }

        /// <summary>
        /// Details of operation fail
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}