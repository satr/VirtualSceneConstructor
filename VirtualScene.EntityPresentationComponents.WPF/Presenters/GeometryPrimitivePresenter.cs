using System.Collections.Generic;
using System.Windows;
using SharpGL.SceneGraph.Core;
using VirtualScene.EntityPresentationComponents.WPF.Commands;
using VirtualScene.EntityPresentationComponents.WPF.Properties;
using VirtualScene.EntityPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Presenters;

namespace VirtualScene.EntityPresentationComponents.WPF.Presenters
{
    /// <summary>
    /// Represents the geometry-primitive on UI
    /// </summary>
    public class GeometryPrimitivePresenter : EntityPresenterBase<SceneElement>
    {
        /// <summary>
        /// Content of the geometry-primitive
        /// </summary>
        /// <returns>The view with the content of the geometry-primitive</returns>
        public override FrameworkElement GetContentView()
        {
            return new GeometryPrimitiveContentView();
        }

        /// <summary>
        /// Control elements to operate with the geometry-primitive
        /// </summary>
        public override IEnumerable<UIElement> TopElements
        {
            get
            {
                yield return CreateButton(Resources.Title_Add_Cube, new AddCubeCommand(SceneContent));
                yield return CreateButton(Resources.Title_Add_Sphere, new AddSphereCommand(SceneContent));
                yield return CreateButton(Resources.Title_Import3D_model, new Import3DModelCommand(SceneContent));
            }
        }
    }
}