using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive
{
    internal class XmlSerializerPool
    {
        private readonly Dictionary<Type, XmlSerializer> _xmlSerializers = new Dictionary<Type, XmlSerializer>();

        public XmlSerializer GetSerializerBy<TObject>(TObject obj)
        {
            return GetSerializerByType(obj.GetType());
        }

        public XmlSerializer GetSerializer<TObject>()
        {
            return GetSerializerByType(typeof(TObject));
        }

        public XmlSerializer GetSerializerByType(Type type)
        {
            if (!_xmlSerializers.ContainsKey(type))
                _xmlSerializers.Add(type, new XmlSerializer(type));
            return _xmlSerializers[type];
        }

    }
}