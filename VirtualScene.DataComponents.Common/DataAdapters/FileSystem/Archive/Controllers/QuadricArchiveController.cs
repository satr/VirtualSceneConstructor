using System.IO.Compression;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{
    /// <summary>
    /// The controller performing archiving operations on the <see cref="Quadric" />.
    /// </summary>
    internal class QuadricArchiveController : SceneElementArchiveControllerBase<Quadric>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadricArchiveController" />
        /// </summary>
        /// <param name="serializerPool">The pool of xml-serializers specific for each used entity-type.</param>
        public QuadricArchiveController(XmlSerializerPool serializerPool)
            : base(serializerPool)
        {
        }

        /// <summary>
        /// Pack the entity of type <see cref="Quadric" /> to the archive.
        /// </summary>
        /// <param name="entity">The entity to be archived.</param>
        /// <param name="archive">The archive where the entity is packed.</param>
        /// <param name="path">The path of the entity in the archive.</param>
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