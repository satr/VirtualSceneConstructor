using System.Collections.Generic;
using System.Windows;
using SharpGL.SceneGraph.Core;
using VirtualScene.EntityPresentationComponents.WPF.Commands;
using VirtualScene.EntityPresentationComponents.WPF.Commands.GeometryCommands;
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
        /// Build the content view.
        /// </summary>
        /// <returns>The view displaying content of an entity.</returns>
        protected override FrameworkElement CreateContentView()
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
                yield return CreateButton(Resources.Title_Spur_Gear, new AddSpurGearCommand(SceneContent));
                yield return CreateButton(Resources.Title_Cylinder, new AddCylinderCommand(SceneContent));
                yield return CreateButton(Resources.Title_Hollow_Cylinder, new AddHollowCylinderCommand(SceneContent));
            }
        }
    }
}