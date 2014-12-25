using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers;
using VirtualScene.Entities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    internal class EntityPackerManager
    {
        private readonly Collection<IEntityPacker> _entityPackers;
        private readonly NullEntityPacker _nullEntityPacker = new NullEntityPacker();

        public EntityPackerManager()
        {
            var serializerPool = new XmlSerializerPool();
            _entityPackers = new Collection<IEntityPacker>
            {
                new EntityPacker<IStage>(serializerPool),
                new EntityPacker<ISceneEntity>(serializerPool),
                new QuadricEntityPacker(serializerPool),
                new PolygonEntityPacker(serializerPool),
                new TypeInfoPacker(serializerPool),
                new EntityPacker<Transformation>(serializerPool)
            };
        }

        public void Pack<T>(T entity, ZipArchive archive, params string[] pathElements)
        {
            GetPackerFor(entity).Pack(entity, archive, pathElements);
        }

        private IEntityPacker GetPackerFor<T>(T entity)
        {
            if (entity == null)
                return _nullEntityPacker;
            return _entityPackers.FirstOrDefault(p => p.EntityType.IsInstanceOfType(entity)) ?? _nullEntityPacker;
        }

        public object UnPack(Type type, Stream stream)
        {
            return GetPackerBy(type).UnPack(type, stream);
        }

        private IEntityPacker GetPackerBy(Type type)
        {
            if (type == null)
                return _nullEntityPacker;
            return _entityPackers.FirstOrDefault(p => p.EntityType.IsAssignableFrom(type)) ?? _nullEntityPacker;
        }
    }
}