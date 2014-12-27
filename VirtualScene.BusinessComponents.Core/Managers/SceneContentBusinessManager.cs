using System;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Pools;
using VirtualScene.BusinessComponents.Core.Properties;
using VirtualScene.Common;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.Core.Managers
{
    /// <summary>
    /// Operations with <see cref="SceneContent" />
    /// </summary>
    public class SceneContentBusinessManager
    {
        /// <summary>
        /// Add a new polygon to the scene
        /// </summary>
        /// <param name="sceneContent">The content of the scene.</param>
        /// <param name="x">The translation X of the scene element.</param>
        /// <param name="y">The translation Y of the scene element.</param>
        /// <param name="z">The translation Z of the scene element.</param>
        /// <param name="name">The name of the scene element.</param>
        public void AddSceneElementInSpace<T>(ISceneContent sceneContent, int x, int y, int z, string name)
            where T : SceneElement, IHasObjectSpace, new()
        {
            AddSceneElementInSpace(sceneContent, new T(), x, y, z, name);
        }

        /// <summary>
        /// Add a new <see cref="SceneElement" /> to the stage
        /// </summary>
        /// <param name="sceneContent">The content of the scene.</param>
        /// <param name="sceneElement">The scene element.</param>
        /// <param name="x">The translation X of the scene element.</param>
        /// <param name="y">The translation Y of the scene element.</param>
        /// <param name="z">The translation Z of the scene element.</param>
        /// <param name="name">The name of the scene element.</param>
        public void AddSceneElementInSpace<T>(ISceneContent sceneContent, T sceneElement, int x, int y, int z, string name)
            where T : SceneElement, IHasObjectSpace
        {
            sceneElement.Transformation.TranslateX += x;
            sceneElement.Transformation.TranslateY += y;
            sceneElement.Transformation.TranslateZ += z;
            AddToStage(new SceneEntity { Name = name, Geometry = sceneElement }, sceneContent);
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
            AddToStage(sceneEntity, sceneContent);
            return actionResult;
        }

        private static void AddToStage(ISceneEntity sceneEntity, ISceneContent sceneContent)
        {
            if (sceneContent.Stage == null)
                throw new InvalidOperationException(Resources.Title_Add_The_stage_of_the_scene_is_not_initialized);
            sceneContent.Stage.Add(sceneEntity);
        }
    }
}
