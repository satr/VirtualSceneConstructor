using SharpGL.SceneGraph.Core;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{
    /// <summary>
    /// The controller performing archiving operations on the <see cref="SceneElement" />.
    /// </summary>
    internal abstract class SceneElementArchiveControllerBase<T> : EntityArchiveController<T>
        where T : SceneElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneElementArchiveControllerBase{T}" />
        /// </summary>
        /// <param name="serializerPool">The pool of xml-serializers specific for each used entity-type.</param>
        protected SceneElementArchiveControllerBase(XmlSerializerPool serializerPool)
            : base(serializerPool)
        {
        }

        /// <summary>
        /// Build the path for the <see cref="IArchiveEntry{T}.Entity" />.
        /// </summary>
        /// <param name="path">The path prior the <see cref="IArchiveEntry{T}.Entity" />.</param>
        /// <returns>The path for the <see cref="IArchiveEntry{T}.Entity" />.</returns>
        protected override string GetEntityPath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Geometry, ArchiveEntryNames.Entry);
        }

        /// <summary>
        /// Build the path for the <see cref="IArchiveEntry{T}.EntityType" />.
        /// </summary>
        /// <param name="path">The path prior the <see cref="IArchiveEntry{T}.EntityType" />.</param>
        /// <returns>The path for the <see cref="IArchiveEntry{T}.EntityType" />.</returns>
        protected override string GetEntityTypePath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Geometry, ArchiveEntryNames.EntryType);
        }
    }
}