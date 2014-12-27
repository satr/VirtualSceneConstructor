using System;
using System.IO;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers
{
    /// <summary>
    /// The controller performing archiving operations on the <see cref="TypeInfo" />.
    /// </summary>
    internal class TypeInfoArchiveController : EntityArchiveController<TypeInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInfoArchiveController" />
        /// </summary>
        /// <param name="serializerPool">The pool of xml-serializers specific for each used entity-type.</param>
        public TypeInfoArchiveController(XmlSerializerPool serializerPool)
            : base(serializerPool)
        {
        }

        /// <summary>
        /// Unpack the object from the archive.
        /// </summary>
        /// <param name="type">The type of the unpacking object.</param>
        /// <param name="stream">The stream of the archive entry.</param>
        /// <returns>The unpacked object.</returns>
        public override object UnPack(Type type, Stream stream)
        {
            var entityTypeInfo = base.UnPack(type, stream) as TypeInfo;
            return entityTypeInfo != null ? entityTypeInfo.CreateEntityType(true) : null;
        }
    }
}