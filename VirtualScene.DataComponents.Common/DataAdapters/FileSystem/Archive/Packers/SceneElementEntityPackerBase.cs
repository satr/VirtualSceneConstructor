using SharpGL.SceneGraph.Core;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    internal abstract class SceneElementEntityPackerBase<T> : EntityPacker<T>
        where T : SceneElement
    {
        protected SceneElementEntityPackerBase(XmlSerializerPool serializerPool) : base(serializerPool)
        {
        }

        protected override string GetEntityPath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Geometry, ArchiveEntryNames.Entry);
        }

        protected override string GetEntityTypePath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Geometry, ArchiveEntryNames.EntryType);
        }
    }
}