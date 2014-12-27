using System;
using System.IO;
using System.IO.Compression;
using VirtualScene.Common;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    internal interface IEntityPacker : IProxyHolder
    {
        void Pack(object obj, ZipArchive archive, params string[] pathElements);
        object UnPack(Type type, Stream stream);
    }
}