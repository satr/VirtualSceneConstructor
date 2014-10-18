using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core.Importers;

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
        /// <param name="sceneEngine"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void AddSceneElementInSpace<T>(ISceneEngine sceneEngine, int x, int y, int z)
            where T : SceneElement, IHasObjectSpace, new()
        {
            var sceneElement = new T();
            sceneElement.Transformation.TranslateX += x;
            sceneElement.Transformation.TranslateY += y;
            sceneElement.Transformation.TranslateZ += z;
            sceneEngine.AddSceneElement(sceneElement);
        }

        /// <summary>
        /// Import of the 3D model from the file to the scene
        /// </summary>
        /// <param name="fullFileName">Path to the file with 3D model</param>
        /// <param name="sceneContent">The content of the scene</param>
        public void Import3DModel(string fullFileName, ISceneContent sceneContent)
        {
            ServiceLocator.Get<WavefrontFormatImporter>().LoadDataToScene(fullFileName, sceneContent.SceneEngine);
        }
    }
}
