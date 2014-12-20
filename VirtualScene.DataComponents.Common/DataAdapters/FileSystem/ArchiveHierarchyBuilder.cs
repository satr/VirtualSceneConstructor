using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    public class ArchiveHierarchyBuilder<T>
    {
        private readonly IArchiveEntry<T> _rootArchiveEntry = new ArchiveEntry<T>("\\");

        public IArchiveEntry<T> GetHierarchy()
        {
            return _rootArchiveEntry;
        }

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
                case StageFileSystemDataAdapter.ArchiveEntryNames.Entry:
                    archiveEntry.Entity = obj;
                    break;
                case StageFileSystemDataAdapter.ArchiveEntryNames.Geometry:
                    if (archiveEntry.Geometry == null)
                        archiveEntry.Geometry = new ArchiveEntry<T>(GetArchiveEntryPath(elements, index));
                    AddArchiveEntity(obj, elements, index + 1, archiveEntry.Geometry);
                    break;
                case StageFileSystemDataAdapter.ArchiveEntryNames.EntryType:
                    archiveEntry.EntityType = obj;
                    break;
                case StageFileSystemDataAdapter.ArchiveEntryNames.Items:
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

    public interface IArchiveEntry<T>
    {
        IList<IArchiveEntry<T>> Items { get; set; }
        T EntityType { get; set; }
        T Entity { get; set; }
        string Path { get; set; }
        IArchiveEntry<T> Geometry { get; set; }
    }

    class ArchiveEntry<T> : IArchiveEntry<T>
    {
        public ArchiveEntry(string path)
        {
            Path = path;
            Items = new List<IArchiveEntry<T>>();
        }

        public string Path { get; set; }
        public IArchiveEntry<T> Geometry { get; set; }
        public IList<IArchiveEntry<T>> Items { get; set; }
        public T EntityType { get; set; }
        public T Entity { get; set; }
    }
}