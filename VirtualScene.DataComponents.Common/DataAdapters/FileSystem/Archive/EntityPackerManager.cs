using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    internal class EntityPackerManager : ProxyManagerBase<IEntityPacker>
    {
        private readonly XmlSerializerPool _serializerPool;

        public EntityPackerManager()
        {
            _serializerPool = new XmlSerializerPool();
        }

        public void Pack<T>(T entity, ZipArchive archive, params string[] pathElements)
        {
            GetProxyHolderFor(entity).Pack(entity, archive, pathElements);
        }

        public object UnPack(Type type, Stream stream)
        {
            return GetProxyHolderBy(type).UnPack(type, stream);
        }

        protected override IEnumerable<IEntityPacker> RegisterProxyHolders()
        {
            yield return new EntityPacker<IStage>(_serializerPool);
            yield return new EntityPacker<ISceneEntity>(_serializerPool);
            yield return new QuadricEntityPacker(_serializerPool);
            yield return new PolygonEntityPacker(_serializerPool);
            yield return new TypeInfoPacker(_serializerPool);
            yield return new EntityPacker<Transformation>(_serializerPool);
        }

        protected override IEntityPacker CreateNullProxyHolder()
        {
            return new NullEntityPacker();
        }
    }
}