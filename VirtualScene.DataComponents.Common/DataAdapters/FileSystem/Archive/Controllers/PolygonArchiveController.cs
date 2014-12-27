using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{

    /// <summary>
    /// The controller performing archiving operations on the <see cref="Polygon" />.
    /// </summary>
    internal class PolygonArchiveController : SceneElementArchiveControllerBase<Polygon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonArchiveController" />
        /// </summary>
        /// <param name="serializerPool">The pool of xml-serializers specific for each used entity-type.</param>
        public PolygonArchiveController(XmlSerializerPool serializerPool)
            : base(serializerPool)
        {
        }
    }
}