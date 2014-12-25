using System;
using System.IO;
using System.IO.Compression;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive.Packers
{
    internal interface IEntityPacker
    {
        Type EntityType { get; }
        void Pack(object obj, ZipArchive archive, params string[] pathElements);
        object UnPack(Type type, Stream stream);
    }
}