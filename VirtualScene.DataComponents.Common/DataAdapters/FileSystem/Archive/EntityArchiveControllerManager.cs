using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Controllers;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    
    internal class EntityArchiveControllerManager : OperationControllerManagerBase<IEntityArchiveController>
    {
        private readonly XmlSerializerPool _serializerPool;

        public EntityArchiveControllerManager()
        {
            _serializerPool = new XmlSerializerPool();
        }

        public void Pack<T>(T entity, ZipArchive archive, params string[] pathElements)
        {
            GetOperationControllerFor(entity).Pack(entity, archive, pathElements);
        }

        public object UnPack(Type type, Stream stream)
        {
            return GetOperationControllerBy(type).UnPack(type, stream);
        }

        protected override IEnumerable<IEntityArchiveController> PreRegisterOperationControllers()
        {
            yield return new EntityArchiveController<IStage>(_serializerPool);
            yield return new EntityArchiveController<ISceneEntity>(_serializerPool);
            yield return new QuadricArchiveController(_serializerPool);
            yield return new PolygonArchiveController(_serializerPool);
            yield return new TypeInfoArchiveController(_serializerPool);
            yield return new EntityArchiveController<Transformation>(_serializerPool);
        }

        protected override IEntityArchiveController CreateNullOperationController()
        {
            return new NullEntityArchiveController();
        }
    }
}