using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Business logic holder
    /// </summary>
    public class BusinessManager
    {
        /// <summary>
        /// Add a new polygon to the scene
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void AddSceneElementInSpace<T>(Scene scene, int x, int y, int z)
            where T : SceneElement, IHasObjectSpace, new()
        {
            var sceneElement = new T();
            sceneElement.Transformation.TranslateX += x;
            sceneElement.Transformation.TranslateY += y;
            sceneElement.Transformation.TranslateZ += z;
            scene.SceneContainer.AddChild(sceneElement);
        }
    }
}
