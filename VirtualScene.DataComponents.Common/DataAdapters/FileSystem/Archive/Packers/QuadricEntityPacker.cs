using System.IO.Compression;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    internal class QuadricEntityPacker: SceneElementEntityPackerBase<Quadric>
    {
        public QuadricEntityPacker(XmlSerializerPool serializerPool):base(serializerPool)
        {
            
        }

        protected override void PackEntity(Quadric entity, ZipArchive archive, string path)
        {
            base.PackEntity(entity, archive, path);
            var transformation = Transformation.CreateForm(entity.Transformation);
            SerializeToArchive(transformation, archive, GetGeometryTransformationEntityPath(path));
            SerializeToArchive(new TypeInfo(transformation), archive, GetGeometryTransformationEntityTypePath(path));
        }

        private static string GetGeometryTransformationEntityPath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Geometry, ArchiveEntryNames.Transformation, ArchiveEntryNames.Entry);
        }

        private static string GetGeometryTransformationEntityTypePath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Geometry, ArchiveEntryNames.Transformation, ArchiveEntryNames.EntryType);
        }
    }
}