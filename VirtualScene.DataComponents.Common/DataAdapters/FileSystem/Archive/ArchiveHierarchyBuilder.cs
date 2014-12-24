using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Properties;

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
        public ActionResult<IArchiveEntry<T>> GetValidatedHierarchy()
        {
            var actionResult = new ActionResult<IArchiveEntry<T>> {Value = _rootArchiveEntry};
            Validate(actionResult);
            return actionResult;
        }

        private static void Validate(ActionResult<IArchiveEntry<T>> actionResult)
        {
            var rootArchiveEntry = actionResult.Value;
            ValidateArchiveEntry(rootArchiveEntry, actionResult);
        }

        private static void ValidateArchiveEntry(IArchiveEntry<T> archiveEntry, IActionResult actionResult)
        {
            if (archiveEntry.Entity == null)
                actionResult.AddError(Resources.Message_Entity_information_is_not_defined_for_the_entry_N, archiveEntry.Path);
            if (archiveEntry.EntityType == null)
                actionResult.AddError(Resources.Message_Entity_type_information_is_not_defined_for_the_entry_N, archiveEntry.Path);
            foreach (var subArchiveEntry in archiveEntry.Items)
                ValidateArchiveEntry(subArchiveEntry, actionResult);
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
                    if (index < elements.Count - 1)
                        AddArchiveEntity(obj, elements, index + 1, archiveEntry);
                    break;
            }
        }

        private static IArchiveEntry<T> AddOrGetArchiveSubEntry(IArchiveEntry<T> archiveEntry, string path)
        {
            var nextArchiveEntry = archiveEntry.Items.FirstOrDefault(ent => ent.Path == path);
            if (nextArchiveEntry != null) 
                return nextArchiveEntry;
            nextArchiveEntry = new ArchiveEntry<T>(path);
            archiveEntry.Items.Add(nextArchiveEntry);
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