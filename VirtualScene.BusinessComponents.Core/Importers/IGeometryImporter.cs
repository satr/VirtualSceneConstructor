using SharpGL.SceneGraph.Core;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.Core.Importers
{
    /// <summary>
    /// The importer of geometry from 3D files
    /// </summary>
    public interface IGeometryImporter
    {
        /// <summary>
        /// Load of 3D geometry from the file
        /// </summary>
        /// <param name="fullFileName">The path to the file with 3D geometry</param>
        /// <param name="sceneEngine">The engine of the scene</param>
        /// <returns></returns>
        ActionResult<SceneElement> LoadGeometry(string fullFileName, ISceneEngine sceneEngine);
    }
}