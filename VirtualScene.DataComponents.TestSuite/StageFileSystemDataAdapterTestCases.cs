using System.IO;
using System.Linq;
using NUnit.Framework;
using SharpGL.SceneGraph.Core;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.Entities;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture]
    class StageFileSystemDataAdapterTestCases
    {
        private StageFileSystemDataAdapter _dataAdapter;
        private IStage _stage;
        private string _testDocumentsFolderPath;

        [SetUp]
        public virtual void SetUp()
        {
            _testDocumentsFolderPath = Helper.GetUniqueName();
            Helper.MockFileSystemEnvironmentDocumentsFolder(_testDocumentsFolderPath);
            Directory.CreateDirectory(_testDocumentsFolderPath);

            _dataAdapter = new StageFileSystemDataAdapter();
            _stage = new Stage { Name = Helper.GetUniqueName() };
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
            if (Directory.Exists(_testDocumentsFolderPath))
                Directory.Delete(_testDocumentsFolderPath, true);
        }

        [Test]
        public void TestAutomaticallyCreateStagesFolderWhenDoesNotExists()
        {
            Assert.IsFalse(Directory.Exists(_dataAdapter.StagesFolderPath));

            _dataAdapter.Save(_stage);

            Assert.IsTrue(Directory.Exists(_dataAdapter.StagesFolderPath));
        }

        [Test]
        public void TestSaveStage()
        {
            _dataAdapter.Save(_stage);
            Assert.IsTrue(File.Exists(_dataAdapter.GetArchiveFilePathFor(_stage)));
        }

        [Test]
        public void TestNegativeResultWhenArchiveNotFound()
        {
            var nonExistingArchivePath = Helper.GetUniqueName();
            var actionResult = _dataAdapter.Load(nonExistingArchivePath);
            Assert.IsNotNull(actionResult);
            Assert.IsFalse(actionResult.Success);
        }

        [Test]
        public void TestLoadEmptyStage()
        {
            _dataAdapter.Save(_stage);
            var actionResult = _dataAdapter.Load(_stage.Name);
            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.Success);
        }

        [Test]
        public void TestLoadStage()
        {
            _dataAdapter.Save(_stage);

            var actionResult = _dataAdapter.Load(_stage.Name);

            Assert.IsTrue(actionResult.Success);
            var loadedAction = actionResult.Value;
            Assert.IsNotNull(loadedAction);
            Assert.AreEqual(_stage, loadedAction);
        }

        [Test]
        public void TestLoadStageWithTwoSceneEntities()
        {
            var geometry1 = Mother.CreateCube();
            var sceneEntity1 = Mother.CreateSceneEntity(geometry1);
            _stage.Items.Add(sceneEntity1);

            var geometry2 = Mother.CreateSphere();
            var sceneEntity2 = Mother.CreateSceneEntity(geometry2);
            _stage.Items.Add(sceneEntity2);
            
            _dataAdapter.Save(_stage);
            
            var actionResult = _dataAdapter.Load(_stage.Name);

            Assert.IsTrue(actionResult.Success);
            var loadedStage = actionResult.Value;
            Assert.IsNotNull(loadedStage);
            Assert.IsNotNull(loadedStage.Items);
            Assert.AreEqual(_stage.Items.Count, loadedStage.Items.Count);
            Assert.AreEqual(_stage, loadedStage);

            var loadedSceneEntity1 = loadedStage.Items.FirstOrDefault(item => item.Name == sceneEntity1.Name);
            Assert.IsNotNull(loadedSceneEntity1);
            Assert.IsNotNull(loadedSceneEntity1.Geometry);
            Assert.AreEqual(sceneEntity1.Geometry.GetType(), loadedSceneEntity1.Geometry.GetType());

            var loadedSceneEntity2 = loadedStage.Items.FirstOrDefault(item => item.Name == sceneEntity2.Name);
            Assert.IsNotNull(loadedSceneEntity2);
            Assert.IsNotNull(loadedSceneEntity2.Geometry);
            Assert.AreEqual(sceneEntity2.Geometry.GetType(), loadedSceneEntity2.Geometry.GetType());
        }

        [Test]
        public void TestLoadCube()
        {
            TestLoadGeometry(Mother.CreateCube());
        }

        [Test]
        public void TestLoadSphere()
        {
            TestLoadGeometry(Mother.CreateSphere());
        }

        [Test]
        public void TestLoadDisk()
        {
            TestLoadGeometry(Mother.CreateDisk());
        }

        [Test]
        public void TestLoadCylinder()
        {
            TestLoadGeometry(Mother.CreateCylinder());
        }

        [Test, Ignore("TODO - Color is not serialized - create wrapping")]
        public void TestLoadLight()
        {
            TestLoadGeometry(Mother.CreateLight());
        }

        //TODO - add more scene element tests

        private void TestLoadGeometry(SceneElement geometry)
        {
            var sceneEntity = Mother.CreateSceneEntity(geometry);
            _stage.Items.Add(sceneEntity);
            _dataAdapter.Save(_stage);

            var actionResult = _dataAdapter.Load(_stage.Name);

            Assert.IsTrue(actionResult.Success);
            var loadedStage = actionResult.Value;
            Assert.AreEqual(1, loadedStage.Items.Count);
            var loadedGeometry = loadedStage.Items[0].Geometry;
            Assert.IsNotNull(loadedGeometry);
            Assert.AreEqual(geometry.GetType(), loadedGeometry.GetType());
            Assert.IsTrue(GeometryEqualityHelper.SceneElementEqual(geometry, loadedGeometry));
        }
    }
}
