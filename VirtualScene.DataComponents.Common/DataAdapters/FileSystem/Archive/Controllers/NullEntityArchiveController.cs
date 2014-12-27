using System;
using System.IO;
using System.IO.Compression;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{
    /// <summary>
    /// The null-controller performing archiving operations on an entity. It is used when no operation controller is found for specific type.
    /// </summary>
    internal class NullEntityArchiveController : EntityArchiveController<object>
    {
        /// <summary>
        /// <see cref="NullEntityArchiveController" />
        /// </summary>
        public NullEntityArchiveController() : base(null)
        {
        }

        /// <summary>
        /// Skip an operation.
        /// </summary>
        /// <param name="entity">The entity to be archived.</param>
        /// <param name="archive">The archive where the entity supposed to be archived.</param>
        /// <param name="path">The path of the entity in the archive.</param>
        protected override void PackEntity(object entity, ZipArchive archive, string path)
        {
            FailingDebugMessage(entity);
        }

        /// <summary>
        /// Unpack the object from the archive.
        /// </summary>
        /// <param name="type">The type of the unpacking object.</param>
        /// <param name="stream">The stream of the archive entry.</param>
        /// <returns>The unpacked object.</returns>
        public override object UnPack(Type type, Stream stream)
        {
            FailingDebugMessage(type);
            return null;
        }

        private static void FailingDebugMessage(object entity)
        {
            System.Diagnostics.Debug.Fail(string.Format(Resources.Message_Null_packer_is_executed_for_N, entity == null ? null : entity.GetType()));
        }
    }
}