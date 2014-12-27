using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using VirtualScene.DataComponents.Common.Properties;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{
    /// <summary>
    /// The controller performing archiving operations on an entity.
    /// </summary>
    internal class EntityArchiveController<T> : IEntityArchiveController
    {
        private readonly XmlSerializerPool _serializerPool;
        private readonly Type _typeKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityArchiveController{T}" />.
        /// </summary>
        /// <param name="serializerPool">The pool of xml-serializers specific for each used entity-type.</param>
        public EntityArchiveController(XmlSerializerPool serializerPool)
        {
            _serializerPool = serializerPool;
            _typeKey = typeof (T);
        }

        /// <summary>
        /// The type-key defining the operation controller.
        /// </summary>
        public Type TypeKey
        {
            get { return _typeKey; }
        }

        /// <summary>
        /// Pack the entity to the archive.
        /// </summary>
        /// <param name="obj">The object to be packed to the archive.</param>
        /// <param name="archive">The archive where the object is packed.</param>
        /// <param name="pathElements">Elements of the path of the object in the archive.</param>
        public void Pack(object obj, ZipArchive archive, params string[] pathElements)
        {
            PackEntity((T) obj, archive, CombinePath(pathElements));
        }

        /// <summary>
        /// Unpack the object from the archive.
        /// </summary>
        /// <param name="type">The type of the unpacking object.</param>
        /// <param name="stream">The stream of the archive entry.</param>
        /// <returns>The unpacked object.</returns>
        public virtual object UnPack(Type type, Stream stream)
        {
            return (T)_serializerPool.GetSerializerByType(type).Deserialize(stream);
        }

        /// <summary>
        /// Pack the entity of type <see cref="T" /> to the archive.
        /// </summary>
        /// <param name="entity">The entity to be archived.</param>
        /// <param name="archive">The archive where the entity is packed.</param>
        /// <param name="path">The path of the entity in the archive.</param>
        protected virtual void PackEntity(T entity, ZipArchive archive, string path)
        {
            SerializeToArchive(entity, archive, GetEntityPath(path));
            SerializeToArchive(new TypeInfo(entity), archive, GetEntityTypePath(path));
        }

        /// <summary>
        /// Build the path for the <see cref="IArchiveEntry{T}.Entity" />.
        /// </summary>
        /// <param name="path">The path prior the <see cref="IArchiveEntry{T}.Entity" />.</param>
        /// <returns>The path for the <see cref="IArchiveEntry{T}.Entity" />.</returns>
        protected virtual string GetEntityPath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.Entry);
        }

        /// <summary>
        /// Build the path for the <see cref="IArchiveEntry{T}.EntityType" />.
        /// </summary>
        /// <param name="path">The path prior the <see cref="IArchiveEntry{T}.EntityType" />.</param>
        /// <returns>The path for the <see cref="IArchiveEntry{T}.EntityType" />.</returns>
        protected virtual string GetEntityTypePath(string path)
        {
            return CombinePath(path, ArchiveEntryNames.EntryType);
        }

        /// <summary>
        /// Serialize the <see cref="entity" /> to the <see cref="archive" /> by the <see cref="path" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of the <see cref="entity" />.</typeparam>
        /// <param name="entity">The entity to be serialized.</param>
        /// <param name="archive">The archive where the entity is serialized.</param>
        /// <param name="path">The path within the archive where the entity is serialized.</param>
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

        /// <summary>
        /// Build the path within an archive.
        /// </summary>
        /// <param name="entryPathElements">The elements of the path within the archive.</param>
        /// <returns>The path within the archive.</returns>
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