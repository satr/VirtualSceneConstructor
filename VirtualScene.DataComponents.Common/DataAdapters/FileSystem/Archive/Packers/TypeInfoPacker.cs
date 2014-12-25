using System;
using System.IO;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    class TypeInfoPacker : EntityPacker<TypeInfo>
    {
        public TypeInfoPacker(XmlSerializerPool serializerPool) : base(serializerPool)
        {
        }

        public override object UnPack(Type type, Stream stream)
        {
            var entityTypeInfo = base.UnPack(type, stream) as TypeInfo;
            return entityTypeInfo != null ? entityTypeInfo.CreateEntityType(true) : null;
        }
    }
}