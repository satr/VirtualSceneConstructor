using System.Linq;
using System.Text;
using NUnit.Framework;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem.Archive;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture]
    class ArchiveHierarchyBuilderTestCases
    {
        private ArchiveHierarchyBuilder<object> _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new ArchiveHierarchyBuilder<object>();
        }

        [Test]
        public void TestInitState()
        {
            Assert.IsNotNull(_builder.GetValidatedHierarchy());
        }

        [Test]
        public void TestGetRootEntryByType()
        {
            var obj = new object();
            _builder.Add(obj, ArchiveEntryNames.Entry);
            var actionResult = _builder.GetValidatedHierarchy();
            var hierarchy = actionResult.Value;
            Assert.IsFalse(actionResult.Success);
            Assert.IsNotNull(hierarchy);
            Assert.IsNotNull(hierarchy.Entity);
            Assert.IsNull(hierarchy.EntityType);
            Assert.AreEqual(0, hierarchy.Items.Count);
        }

        [Test]
        public void TestGetRootEntryByEntryType()
        {
            var obj = new object();
            _builder.Add(obj, ArchiveEntryNames.EntryType);
            var actionResult = _builder.GetValidatedHierarchy();
            var hierarchy = actionResult.Value;
            Assert.IsFalse(actionResult.Success);
            Assert.IsNotNull(hierarchy.EntityType);
            Assert.AreEqual(obj, hierarchy.EntityType);
            Assert.IsNull(hierarchy.Entity);
            Assert.AreEqual(0, hierarchy.Items.Count);
        }

        [Test]
        public void TestGetEntryInItemsLevel2()
        {
            var obj = new object();
            _builder.Add(obj, BuildPath(ArchiveEntryNames.Items, Helper.GetUniqueName(), ArchiveEntryNames.Entry));

            var actionResult = _builder.GetValidatedHierarchy();
            var hierarchy = actionResult.Value;
            Assert.IsFalse(actionResult.Success);
            Assert.IsNull(hierarchy.Entity);
            Assert.IsNull(hierarchy.EntityType);
            Assert.IsNotNull(hierarchy.Items);
            Assert.AreEqual(1, hierarchy.Items.Count);
            
            var archiveEntity = hierarchy.Items[0];
            Assert.IsNotNull(archiveEntity);
            Assert.AreEqual(obj, archiveEntity.Entity);
            Assert.IsNull(archiveEntity.EntityType);
        }

        [Test]
        public void TestGetTwoEntriesInItemsLevel2()
        {
            var obj1 = new object();
            var entityName1 = Helper.GetUniqueName();
            _builder.Add(obj1, BuildPath(ArchiveEntryNames.Items, entityName1, ArchiveEntryNames.Entry));
            var obj2 = new object();
            var entityName2 = Helper.GetUniqueName();
            _builder.Add(obj2, BuildPath(ArchiveEntryNames.Items, entityName2, ArchiveEntryNames.Entry));

            var actionResult = _builder.GetValidatedHierarchy();
            var hierarchy = actionResult.Value;

            Assert.IsFalse(actionResult.Success);
            
            var archiveEntity1 = hierarchy.Items.FirstOrDefault(en => en.Path.EndsWith(entityName1));
            Assert.IsNotNull(archiveEntity1);
            Assert.AreEqual(obj1, archiveEntity1.Entity);
            Assert.IsNull(archiveEntity1.EntityType);
            

            var archiveEntity2 = hierarchy.Items.FirstOrDefault(en => en.Path.EndsWith(entityName2));
            Assert.IsNotNull(archiveEntity2);
            Assert.AreEqual(obj2, archiveEntity2.Entity);
            Assert.IsNull(archiveEntity2.EntityType);
        }

        [Test]
        public void TestGetTwoEntriesInItemsLevel2AndOneInItemsLevel3()
        {
            var obj1 = new object();
            var entityName1 = Helper.GetUniqueName();
            _builder.Add(obj1, BuildPath(ArchiveEntryNames.Items, entityName1, ArchiveEntryNames.Entry));
            var obj2 = new object();
            var entityName2 = Helper.GetUniqueName();
            _builder.Add(obj2, BuildPath(ArchiveEntryNames.Items, entityName2, ArchiveEntryNames.Entry));
            var obj3 = new object();
            var entityName3 = Helper.GetUniqueName();
            _builder.Add(obj3, BuildPath(ArchiveEntryNames.Items, entityName2, ArchiveEntryNames.Items, entityName3, ArchiveEntryNames.Entry));

            var actionResult = _builder.GetValidatedHierarchy();
            var hierarchy = actionResult.Value;

            Assert.IsFalse(actionResult.Success);

            var archiveEntity2 = hierarchy.Items.FirstOrDefault(en => en.Path.EndsWith(entityName2));
            Assert.IsNotNull(archiveEntity2);
            Assert.AreEqual(obj2, archiveEntity2.Entity);
            Assert.IsNull(archiveEntity2.EntityType);
            Assert.IsNotNull(archiveEntity2.Items);
            Assert.AreEqual(1, archiveEntity2.Items.Count);

            var archiveEntity3 = archiveEntity2.Items.FirstOrDefault(en => en.Path.EndsWith(entityName3));
            Assert.IsNotNull(archiveEntity3);
            Assert.AreEqual(obj3, archiveEntity3.Entity);
            Assert.IsNull(archiveEntity3.EntityType);
        }

        [Test]
        public void TestGetEntryInItemsLevel2WithGeometry()
        {
            var sceneEntityArchiveEntryName = Helper.GetUniqueName();
            var geometry = new object();
            _builder.Add(geometry, BuildPath(ArchiveEntryNames.Items, sceneEntityArchiveEntryName, ArchiveEntryNames.Geometry, ArchiveEntryNames.Entry));
            var geometryType = new object();
            _builder.Add(geometryType, BuildPath(ArchiveEntryNames.Items, sceneEntityArchiveEntryName, ArchiveEntryNames.Geometry, ArchiveEntryNames.EntryType));

            var actionResult = _builder.GetValidatedHierarchy();
            var hierarchy = actionResult.Value;

            Assert.IsFalse(actionResult.Success);

            var sceneEntityArchiveEntry = hierarchy.Items.FirstOrDefault(en => en.Path.EndsWith(sceneEntityArchiveEntryName));
            Assert.IsNotNull(sceneEntityArchiveEntry);
            Assert.IsNotNull(sceneEntityArchiveEntry.Geometry);
            var geometryArchiveEntry = sceneEntityArchiveEntry.Geometry;
            Assert.AreEqual(geometry, geometryArchiveEntry.Entity);
            Assert.AreEqual(geometryType, geometryArchiveEntry.EntityType);
            Assert.IsNull(geometryArchiveEntry.Geometry);
            Assert.IsNotNull(geometryArchiveEntry.Items);
            Assert.AreEqual(0, geometryArchiveEntry.Items.Count);
        }

        [Test]
        public void TestAddInvalidEntry()
        {
            var obj = new object();
            _builder.Add(obj, BuildPath(Helper.GetUniqueName()));

            var actionResult = _builder.GetValidatedHierarchy();
            Assert.IsFalse(actionResult.Success);
        }

        private static string BuildPath(params string[] pathElements)
        {
            var builder = new StringBuilder();
            foreach (var pathElement in pathElements)
            {
                if (builder.Length > 0)
                    builder.Append(@"\");
                builder.Append(pathElement);
            }
            return builder.ToString();
        }
    }
}
