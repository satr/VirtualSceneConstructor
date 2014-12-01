using VirtualScene.BusinessComponents.Core.Importers;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.Core.Pools
{
    /// <summary>
    /// The pool providing geometry importers
    /// </summary>
    public class GeometryImportersPool
    {
        /// <summary>
        /// Gets the importer of geometry from 3D files with Wavefront format
        /// </summary>
        /// <returns>THe importer to the wavefront-format</returns>
        public virtual IGeometryImporter GetWavefrontFormatImporter()
        {
            return ServiceLocator.Get<WavefrontFormatImporter>();
        }
    }
}