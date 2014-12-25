using System;
using System.IO;
using System.IO.Compression;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    internal class NullEntityPacker : EntityPacker<object>
    {
        public NullEntityPacker() : base(null)
        {
        }

        protected override void PackEntity(object entity, ZipArchive archive, string path)
        {
            FailingDebugMessage(entity);
        }

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