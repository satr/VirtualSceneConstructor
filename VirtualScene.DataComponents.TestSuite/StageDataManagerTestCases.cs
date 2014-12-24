using Moq;
using NUnit.Framework;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;
using VirtualScene.Entities;
using VirtualScene.EntityDataComponents;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture]
    public class StageDataManagerTestCases
    {
        private StageDataManager _stageDataManager;
        private Mock<IStage> _stageMock;
        private string _stageName;
        private Mock<IFileSystemDataAdapter<IStage>> _dataAdapterMock;

        [SetUp]
        public void SetUp()
        {
            _stageMock = new Mock<IStage>();
            _stageName = Helper.GetUniqueName();
            _stageMock.SetupGet(p => p.Name).Returns(_stageName);
            _dataAdapterMock = new Mock<IFileSystemDataAdapter<IStage>>();
            _stageDataManager = new StageDataManager(_dataAdapterMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }

        [Test]
        public void TestSaveStageWithSameName()
        {
            _stageDataManager.Save(_stageMock.Object);
            _dataAdapterMock.Verify(m => m.Save(_stageMock.Object));
        }        
        
        [Test]
        public void TestLoadStage()
        {
            _stageDataManager.Load(_stageName);
            _dataAdapterMock.Verify(m => m.Load(_stageName));
        }
    }
}
