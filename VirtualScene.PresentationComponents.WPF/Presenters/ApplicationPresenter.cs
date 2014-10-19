using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.PresentationComponents.WPF.Commands;
using VirtualScene.PresentationComponents.WPF.Properties;

namespace VirtualScene.PresentationComponents.WPF.Presenters
{
    /// <summary>
    /// ApplicationPresenter provides UI components for application
    /// </summary>
    public class ApplicationPresenter
    {
        /// <summary>
        /// Creates a new instance of the presenter
        /// </summary>
        public ApplicationPresenter()
        {
            SceneContent = ServiceLocator.Get<SceneContentFactory>().Create();
            InitTopElements(SceneContent);
            SceneContent.Stage = new Stage();
        }

        /// <summary>
        /// The content of the scene
        /// </summary>
        public ISceneContent SceneContent { get; private set; }

        private void InitTopElements(ISceneContent sceneContent)
        {
            TopElements = new List<UIElement>
            {
                CreateButton(Resources.Title_Add_Cube, new AddCubeCommand(sceneContent)),
                CreateButton(Resources.Title_Add_Sphere, new AddSphereCommand(sceneContent)),
                CreateButton(Resources.Title_Import3D_model, new Import3DModelCommand(sceneContent)),
                CreateButton(Resources.Title_Add_Arc_Camera, new AddCameraCommand<ArcBallCamera>(sceneContent, Resources.Title_Add_Arc_Camera)),
                CreateButton(Resources.Title_Add_Free_Camera, new AddCameraCommand<LookAtCamera>(sceneContent, Resources.Title_Add_Free_Camera)),
                CreateButton(Resources.Title_Help, new ShowHelpCommand()),
            };
        }


        private static Button CreateButton(string title, ICommand command)
        {
            var button = new Button
            {
                Width = 60, Height = 48, Content = title, Command = command
            };
            return button;
        }

        /// <summary>
        /// Elements Locate at the top of main window
        /// </summary>
        public IList<UIElement> TopElements { get; private set; }

    }
}