using System.Collections.Generic;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    /// <summary>
    /// The entries of an entity in an archive. Used for processing hierarchical archive content.
    /// </summary>
    /// <typeparam name="T">Type of the archive entry.</typeparam>
    internal class ArchiveEntry<T> : IArchiveEntry<T>
    {
        /// <summary>
        /// Createa a new instance of the ArchiveEntry.
        /// </summary>
        /// <param name="path"></param>
        public ArchiveEntry(string path)
        {
            Path = path;
            Items = new List<IArchiveEntry<T>>();
        }

        /// <summary>
        /// Sub-entries of the hierarchycal entry.
        /// </summary>
        public IList<IArchiveEntry<T>> Items { get; set; }

        /// <summary>
        /// The path of the entry within the archive.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The entry with geometry of the entity.
        /// </summary>
        public IArchiveEntry<T> Geometry { get; set; }

        /// <summary>
        /// The transformation of the entity.
        /// </summary>
        public IArchiveEntry<T> Transformation { get; set; }

        /// <summary>
        /// The entry with the type of entity.
        /// </summary>
        public T EntityType { get; set; }
        
        /// <summary>
        /// The entry with the entity content.
        /// </summary>
        public T Entity { get; set; }
    }
}