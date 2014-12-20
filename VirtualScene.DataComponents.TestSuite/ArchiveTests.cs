using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using NUnit.Framework;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture, Ignore]
    public class ArchiveTests
    {
        private static TestEntity _entity;
        private static string _archiveFileFullName;
        private static string _folderName;
        private const string EntryFolder = @"z";
        private const string EntryName = @"entry.xml";
        private const string EntryType = @"type.xml";

        [TestFixtureSetUp]
        public static void TextFixtureSetUp()
        {
            _folderName = Helper.CreateTempFolder();
            _archiveFileFullName = string.Format(@"{0}\{1}.zip", _folderName, Helper.GetUniqueName());
            _entity = Mother.CreateTestEntity();
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_folderName))
                Directory.Delete(_folderName, true);
        }

        [Test]
        public void TestPackUnpackEntity()
        {
            using (var fileStream = File.Create(_archiveFileFullName))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                var entry = CreateEntry(archive, EntryFolder, EntryName);

                using (var stream = entry.Open())
                    GetXmlSerializer(_entity.GetType()).Serialize(stream, _entity);

                var entityType = _entity.GetType();
                entry = CreateEntry(archive, EntryFolder, EntryType);
                using (var stream = entry.Open())
                {
                    GetXmlSerializer(typeof (string)).Serialize(stream, entityType.ToString());
                }
            }

            Assert.IsTrue(File.Exists(_archiveFileFullName));


            TestEntity restoredEntity;

            using (var fileStream = File.OpenRead(_archiveFileFullName))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                string entityTypeString;
                Type entryType = null;
                var entryTypeEntry = GetEntry(archive, EntryFolder, EntryType);
                using (var stream = entryTypeEntry.Open())
                {
                    entityTypeString = GetXmlSerializer(typeof (string)).Deserialize(stream) as string;
                    if (entityTypeString != null)
                        entryType = Type.GetType(entityTypeString);
                }

                if (entryType == null)
                    Assert.Fail("Type is not deserialized properly from the type string \"{0}\"", entityTypeString);

                var entry = GetEntry(archive, EntryFolder, EntryName);
                using (var stream = entry.Open())
                    restoredEntity = GetXmlSerializer(entryType).Deserialize(stream) as TestEntity;
            }

            Assert.IsNotNull(restoredEntity);
            Assert.AreEqual(_entity, restoredEntity);

        }

        private static XmlSerializer GetXmlSerializer(Type type)
        {
            return new XmlSerializer(type);
        }

        private static ZipArchiveEntry CreateEntry(ZipArchive archive, string entryFolder, string entryName)
        {
            return archive.CreateEntry(GetEntryName(entryFolder, entryName));
        }

        private static ZipArchiveEntry GetEntry(ZipArchive archive, string entryFolder, string entryName)
        {
            return archive.GetEntry(GetEntryName(entryFolder, entryName));
        }

        private static string GetEntryName(string entryFolder, string entryName)
        {
            return entryFolder + @"\" + entryName;
        }
    }

}
