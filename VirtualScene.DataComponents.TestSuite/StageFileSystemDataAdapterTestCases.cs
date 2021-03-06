﻿using System.IO;
using System.Linq;
using NUnit.Framework;
using SharpGL.SceneGraph.Core;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.DataComponents.Common.Exceptions;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture]
    class StageFileSystemDataAdapterTestCases
    {
        private StageFileSystemDataAdapter _dataAdapter;
        private IStage _stage;
        private string _stageFolderPath;

        [SetUp]
        public virtual void SetUp()
        {
            _stageFolderPath = Helper.GetUniqueName();
            Helper.MockFileSystemEnvironmentDocumentsFolder(_stageFolderPath);

            _dataAdapter = new StageFileSystemDataAdapter();
            _stage = new Stage { Name = Helper.GetUniqueName() };
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
            if (Directory.Exists(_stageFolderPath))
                Directory.Delete(_stageFolderPath, true);
        }

        [Test, ExpectedException(typeof(DataAdapterConfigurationException))]
        public void TestFailOnSaveWhenEntityFolderPathIsNotDefined()
        {
            _dataAdapter.Save(_stage);
        }

        [Test, ExpectedException(typeof(DataAdapterConfigurationException))]
        public void TestFailOnLoadWhenEntityFolderPathIsNotDefined()
        {
            _dataAdapter.Load(Helper.GetUniqueName());
        }

        [Test]
        public void TestAutomaticallyCreateStagesFolderWhenDoesNotExists()
        {
            Assert.IsFalse(Directory.Exists(_stageFolderPath));

            _dataAdapter.EntityFolderPath = _stageFolderPath;
            _dataAdapter.Save(_stage);

            Assert.IsTrue(Directory.Exists(_stageFolderPath));
        }

        [Test]
        public void TestSaveStage()
        {
            _dataAdapter.EntityFolderPath = _stageFolderPath;
            _dataAdapter.Save(_stage);
            Assert.IsTrue(File.Exists(Path.Combine(_stageFolderPath, _stage.Name + Constants.Stage.ArchiveFileExtension)));
        }

        [Test]
        public void TestNegativeResultWhenArchiveNotFound()
        {
            _dataAdapter.EntityFolderPath = _stageFolderPath;
            var nonExistingArchivePath = Helper.GetUniqueName();
            var actionResult = _dataAdapter.Load(nonExistingArchivePath);
            Assert.IsNotNull(actionResult);
            Assert.IsFalse(actionResult.Success);
        }

        [Test]
        public void TestLoadEmptyStage()
        {
            _dataAdapter.EntityFolderPath = _stageFolderPath;
            _dataAdapter.Save(_stage);
            var actionResult = _dataAdapter.Load(_stage.Name);
            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.Success);
        }

        [Test]
        public void TestLoadStage()
        {
            _dataAdapter.EntityFolderPath = _stageFolderPath;
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
            var sceneEntity1 = Mother.CreateCubeEntity();
            _stage.Add(sceneEntity1);

            var sceneEntity2 = Mother.CreateCubeEntity();
            _stage.Add(sceneEntity2);

            _dataAdapter.EntityFolderPath = _stageFolderPath;
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
            TestLoadGeometry(Mother.CreateCubeEntity());
        }

        [Test]
        public void TestLoadSphere()
        {
            TestLoadGeometry(Mother.CreateCubeEntity());
        }

        [Test]
        public void TestLoadDisk()
        {
            TestLoadGeometry(Mother.CreateCubeEntity());
        }

        [Test]
        public void TestLoadCylinder()
        {
            TestLoadGeometry(Mother.CreateCubeEntity());
        }

        [Test, Ignore("TODO - Color is not serialized - create wrapping")]
        public void TestLoadLight()
        {
            TestLoadGeometry(Mother.CreateCubeEntity());
        }

        //TODO - add more scene element tests

        private void TestLoadGeometry(ISceneEntity sceneEntity)
        {
            _stage.Add(sceneEntity);
            _dataAdapter.EntityFolderPath = _stageFolderPath;
            _dataAdapter.Save(_stage);

            var actionResult = _dataAdapter.Load(_stage.Name);

            Assert.IsTrue(actionResult.Success);
            var loadedStage = actionResult.Value;
            Assert.AreEqual(1, loadedStage.Items.Count);
            var loadedGeometry = loadedStage.Items[0].Geometry;
            Assert.IsNotNull(loadedGeometry);
            Assert.AreEqual(sceneEntity.Geometry.GetType(), loadedGeometry.GetType());
            Assert.IsTrue(GeometryEqualityHelper.SceneElementEqual(sceneEntity.Geometry, loadedGeometry));
        }
    }
}
