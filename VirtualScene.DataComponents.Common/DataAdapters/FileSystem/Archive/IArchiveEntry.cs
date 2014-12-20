using System.Collections.Generic;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    /// <summary>
    /// The entries of an entity in an archive. Used for processing hierarchical archive content.
    /// </summary>
    /// <typeparam name="T">Type of the archive entry.</typeparam>
    public interface IArchiveEntry<T>
    {
        /// <summary>
        /// Sub-entries of the hierarchycal entry.
        /// </summary>
        IList<IArchiveEntry<T>> Items { get; set; }
        /// <summary>
        /// The entry with the type of entity.
        /// </summary>
        T EntityType { get; set; }
        /// <summary>
        /// The entry with the entity content.
        /// </summary>
        T Entity { get; set; }
        /// <summary>
        /// The path of the entry within the archive.
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// The entry with geometry of the entity.
        /// </summary>
        IArchiveEntry<T> Geometry { get; set; }
    }
}