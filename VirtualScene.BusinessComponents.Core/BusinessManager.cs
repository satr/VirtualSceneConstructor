using System;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Importers;
using VirtualScene.BusinessComponents.Core.Pools;
using VirtualScene.BusinessComponents.Core.Properties;

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
        /// <param name="name">he name of the entity in the scene</param>
        /// <param name="fullFileName">Path to the file with 3D model</param>
        /// <param name="sceneContent">The content of the scene</param>
        public IActionResult Import3DModel(string name, string fullFileName, ISceneContent sceneContent)
        {
            var actionResult = ServiceLocator.Get<GeometryImportersPool>()
                                         .GetWavefrontFormatImporter()
                                         .LoadGeometry(fullFileName, sceneContent.SceneEngine);
            if(!actionResult.Success)
                return actionResult;
            var sceneEntity = new SceneEntity { Name = name, Geometry = actionResult.Value };
            sceneContent.Add(sceneEntity);
            return actionResult;
        }
    }
}
