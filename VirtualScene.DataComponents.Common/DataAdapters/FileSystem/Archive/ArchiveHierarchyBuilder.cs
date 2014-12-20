using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    /// <summary>
    /// The builder to construct hierarchy of entries from an archive.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ArchiveHierarchyBuilder<T>
    {
        private readonly IArchiveEntry<T> _rootArchiveEntry = new ArchiveEntry<T>("\\");

        /// <summary>
        /// The hierarchy of entries.
        /// </summary>
        /// <returns>The root archive-entry.</returns>
        public IArchiveEntry<T> GetHierarchy()
        {
            return _rootArchiveEntry;
        }

        /// <summary>
        /// Add an archive-entry with its path.
        /// </summary>
        /// <param name="obj">The entry in the archive.</param>
        /// <param name="path">The path of the entry within archive.</param>
        public void Add(T obj, string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                return;
            var elements = path.Split('\\');
            if(elements.Length == 0)
                return;
            AddArchiveEntity(obj, elements, 0, _rootArchiveEntry);
        }

        private static void AddArchiveEntity(T obj, IList<string> elements, int index, IArchiveEntry<T> archiveEntry)
        {
            switch (elements[index])
            {
                case ArchiveEntryNames.Entry:
                    archiveEntry.Entity = obj;
                    break;
                case ArchiveEntryNames.Geometry:
                    if (archiveEntry.Geometry == null)
                        archiveEntry.Geometry = new ArchiveEntry<T>(GetArchiveEntryPath(elements, index));
                    AddArchiveEntity(obj, elements, index + 1, archiveEntry.Geometry);
                    break;
                case ArchiveEntryNames.EntryType:
                    archiveEntry.EntityType = obj;
                    break;
                case ArchiveEntryNames.Items:
                    if (elements.Count <= index)
                        return;
                    var path = GetArchiveEntryPath(elements, index + 1);
                    var archiveSubEntry = AddOrGetArchiveSubEntry(archiveEntry, path);
                    AddArchiveEntity(obj, elements, index + 1, archiveSubEntry);
                    break;
                default:
                    AddArchiveEntity(obj, elements, index + 1, archiveEntry);
                    break;
            }
        }

        private static IArchiveEntry<T> AddOrGetArchiveSubEntry(IArchiveEntry<T> archiveEntry, string path)
        {
            var nextArchiveEntry = archiveEntry.Items.FirstOrDefault(ent => ent.Path == path);
            if (nextArchiveEntry == null)
            {
                nextArchiveEntry = new ArchiveEntry<T>(path);
                archiveEntry.Items.Add(nextArchiveEntry);
            }
            return nextArchiveEntry;
        }

        private static string GetArchiveEntryPath(IList<string> elements, int index)
        {
            var builder = new StringBuilder(index);
            for (var i = 0; i <= index; i++)
            {
                if (i > 0)
                    builder.Append(@"\");
                builder.Append(elements[i]);
            }
            return builder.ToString();
        }
    }
}