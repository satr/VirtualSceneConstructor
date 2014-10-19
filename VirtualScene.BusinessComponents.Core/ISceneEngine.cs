using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// Interface to the 3D scene engine.
    /// </summary>
    public interface ISceneEngine
    {
        /// <summary>
        /// The update rate for external modifications.
        /// </summary>
        int UpdateRate { get; }

        /// <summary>
        /// Cameras in the scene
        /// </summary>
        ObservableCollection<Camera> Cameras { get; }

        /// <summary>
        /// Set the update rate for external modifications.
        /// </summary>
        /// <param name="value">the update rate (in milliseconds). Minimum: 1 ms</param>
        /// <returns></returns>
        bool SetUpdateRate(int value);

        /// <summary>
        /// Creates the viewport with a scene. All previously added acene elements are automatically added to the new vieport.
        /// </summary>
        /// <returns>Returnd the new viewport</returns>
        SceneViewport CreateViewport();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        void Move(float dx, float dy, float dz);

        /// <summary>
        /// Add an entity to the scene
        /// </summary>
        /// <param name="sceneEntity">The entity to be added to the scene</param>
        void AddSceneEntity(ISceneEntity sceneEntity);

        /// <summary>
        /// Remove an entity from the scene
        /// </summary>
        /// <param name="sceneEntity">The entity to be removed from the scene</param>
        void RemoveSceneEntity(ISceneEntity sceneEntity);

        /// <summary>
        /// Clear the scene
        /// </summary>
        void Clear();

        /// <summary>
        /// Count of SceneElements in the scene
        /// </summary>
        int SceneElementsCount { get; }

        /// <summary>
        /// Gets the asset list of epecified type
        /// </summary>
        /// <typeparam name="T">Type of the asset</typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAssets<T>() where T : Asset;

        /// <summary>
        /// Add a new asset to the scene
        /// </summary>
        /// <param name="asset"></param>
        void AddAsset<T>(T asset) where T : Asset;

        /// <summary>
        /// Load existing or create a new texture.
        /// </summary>
        /// <param name="path">Path to the file of texture</param>
        /// <param name="textureFile">File name of texture</param>
        /// <returns>The texture</returns>
        Texture LoadOrCreateTexture(string path, string textureFile);
    }
}