using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core.Importers;

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
        /// <returns></returns>
        public virtual IGeometryImporter GetWavefrontFormatImporter()
        {
            return ServiceLocator.Get<WavefrontFormatImporter>();
        }
    }
}