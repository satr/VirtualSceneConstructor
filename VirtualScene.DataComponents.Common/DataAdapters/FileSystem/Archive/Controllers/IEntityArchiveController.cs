using System;
using System.IO;
using System.IO.Compression;
using VirtualScene.Common;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{
    /// <summary>
    /// The controller performing archiving operations on an entity.
    /// </summary>
    internal interface IEntityArchiveController : IOperationController
    {
        /// <summary>
        /// Pack the entity to the archive.
        /// </summary>
        /// <param name="obj">The object to be packed to the archive.</param>
        /// <param name="archive">The archive where the object is packed.</param>
        /// <param name="pathElements">Elements of the phe path of the object in the archive.</param>
        void Pack(object obj, ZipArchive archive, params string[] pathElements);
        
        /// <summary>
        /// Unpack the object from the archive.
        /// </summary>
        /// <param name="type">The type of the unpacking object.</param>
        /// <param name="stream">The stream of the archive entry.</param>
        /// <returns>The unpacked object.</returns>
        object UnPack(Type type, Stream stream);
    }
}