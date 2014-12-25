using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using VirtualScene.DataComponents.Common.Properties;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    internal class EntityPacker<T> : IEntityPacker
    {
        private readonly XmlSerializerPool _serializerPool;
        private readonly Type _entityType;

        public EntityPacker(XmlSerializerPool serializerPool)
        {
            _serializerPool = serializerPool;
            _entityType = typeof (T);
        }

        public Type EntityType
        {
            get { return _entityType; }
        }

        public void Pack(object obj, ZipArchive archive, params string[] pathElements)
        {
            PackEntity((T) obj, archive, CombinePath(pathElements));
        }

        public virtual object UnPack(Type type, Stream stream)
        {
            return (T)_serializerPool.GetSerializerByType(type).Deserialize(stream);
        }

        protected virtual void PackEntity(T entity, ZipArchive archive, string path)
        {
            SerializeToArchive(entity, archive, GetEntityPath(path));
            SerializeToArchive(new TypeInfo(entity), archive, GetEntityTypePath(path));
        }

        protected virtual string GetEntityPath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Entry);
        }

        protected virtual string GetEntityTypePath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.EntryType);
        }

        protected void SerializeToArchive<TEntity>(TEntity entity, ZipArchive archive, string path)
        {
            var entityEntry = CreateEntry(archive, path);
            using (var stream = entityEntry.Open())
                _serializerPool.GetSerializerBy(entity).Serialize(stream, entity);
        }

        private static ZipArchiveEntry CreateEntry(ZipArchive archive, string entryPath)
        {
            return archive.CreateEntry(entryPath);
        }

        protected static string CombinePath(params string[] entryPathElements)
        {
            if (entryPathElements.Length == 0)
                throw new ArgumentException(Resources.Message_Archive_No_entry_elements_is_defined_in_the_archive_path, "entryPathElements");

            var builder = new StringBuilder();
            foreach (var entryPathElement in entryPathElements.Where(entryPathElement => !String.IsNullOrWhiteSpace(entryPathElement)))
            {
                if (builder.Length > 0)
                    builder.Append(@"\");
                builder.Append(entryPathElement);
            }
            return builder.ToString();
        }
    }
}