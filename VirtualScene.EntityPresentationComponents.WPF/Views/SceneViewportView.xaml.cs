﻿using System.Windows.Input;
using VirtualScene.EntityPresentationComponents.WPF.ViewModels;

namespace VirtualScene.EntityPresentationComponents.WPF.Views
{
    /// <summary>
    ///     Interaction logic for SceneViewportView.xaml
    /// </summary>
    public partial class SceneViewportView
    {
        private readonly SceneViewportViewModel _sceneViewportViewModel;

        /// <summary>
        ///     Createts a new instance of the SceneViewportView user-control
        /// </summary>
        /// <param name="viewModel">The view-model of the viewport view</param>
        public SceneViewportView(SceneViewportViewModel viewModel)
        {
            InitializeComponent();
            DataContext = _sceneViewportViewModel = viewModel;
            MouseDown += OnMouseDownEventHandler;
            MouseUp += OnMouseUpEventHandler;
            MouseMove += OnMouseMoveEventHandler;
        }

        private void OnMouseMoveEventHandler(object s, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _sceneViewportViewModel.LeftButtonMouseMove(e.GetPosition(this));
            }
        }

        private void OnMouseUpEventHandler(object s, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                _sceneViewportViewModel.LeftButtonMouseUp(e.GetPosition(this));
            Mouse.OverrideCursor = null;
        }

        private void OnMouseDownEventHandler(object s, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            if (e.LeftButton == MouseButtonState.Pressed)
                _sceneViewportViewModel.LeftButtonMouseDown(e.GetPosition(this));
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _sceneViewportViewModel.NotifyViewTemplateApplied();
        }
    }
}
