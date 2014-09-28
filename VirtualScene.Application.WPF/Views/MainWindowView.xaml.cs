using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VirtualScene.Application.WPF.ViewModels;

namespace VirtualScene.Application.WPF.Views
{
    /// <summary>
    /// The main window of the application
    /// </summary>
    public partial class MainWindowView
    {
        /// <summary>
        /// Creates a new instance of the main window of the application
        /// </summary>
        public MainWindowView()
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            foreach (var element in viewModel.TopElements)
            {
                TopPanel.Children.Add(element);
            }
        }
    }
}
