using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    class PolygonEntityPacker : SceneElementEntityPackerBase<Polygon>
    {
        public PolygonEntityPacker(XmlSerializerPool serializerPool) : base(serializerPool)
        {
        }
    }
}