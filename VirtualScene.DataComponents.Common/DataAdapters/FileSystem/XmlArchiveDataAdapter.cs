using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    /// <summary>
    /// The data adapter operating with xml-data archived to a zip-file.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class XmlArchiveDataAdapter<T>: FileSystemDataAdapter<T>
    {
        private readonly Dictionary<Type, XmlSerializer> _xmlSerializers = new Dictionary<Type, XmlSerializer>();

        protected static void CreateArchiveIfDoesNotExist(string archiveFilePath)
        {
            if (File.Exists(archiveFilePath))
                return;
            using (var fileStream = new FileStream(archiveFilePath, FileMode.Create))
            using (new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                //create an empty archive
            }
        }

        protected XmlSerializer GetSerializerBy<TObject>(TObject obj)
        {
            return GetSerializerByType(obj.GetType());
        }

        protected XmlSerializer GetSerializer<TObject>()
        {
            return GetSerializerByType(typeof(TObject));
        }

        protected XmlSerializer GetSerializerByType(Type type)
        {
            if (!_xmlSerializers.ContainsKey(type))
                _xmlSerializers.Add(type, new XmlSerializer(type));
            return _xmlSerializers[type];
        }

        protected static ZipArchiveEntry CreateEntry(ZipArchive archive, string entryPath)
        {
            return archive.CreateEntry(entryPath);
        }

        protected static ZipArchiveEntry GetEntry(ZipArchive archive, string entryPath)
        {
            return archive.GetEntry(entryPath);
        }
    }
}
