using System;
using System.IO;
using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture]
    class FileSystemDataAdapter
    {
        private StageFileSystemDataAdapter _dataAdapter;
        private Stage _stage;
        private string _testDocumentsFolderPath;

        [SetUp]
        public void SetUp()
        {
            _testDocumentsFolderPath = Helper.GetUniqueName();
            MockFileSystemEnvironmentDocumentsFolder(_testDocumentsFolderPath);
            Directory.CreateDirectory(_testDocumentsFolderPath);

            _dataAdapter = new StageFileSystemDataAdapter();
            _stage = new Stage {Name = Helper.GetUniqueName()};
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

        private static void MockFileSystemEnvironmentDocumentsFolder(string path)
        {
            var fileSystemEnvironmentWrapperMock = new Mock<FileSystemEnvironmentWrapper>();
            fileSystemEnvironmentWrapperMock.Setup(m => m.GetFolderPath(Environment.SpecialFolder.CommonDocuments)).Returns(path);
            ServiceLocator.Set(fileSystemEnvironmentWrapperMock.Object);
        }
    }
}
