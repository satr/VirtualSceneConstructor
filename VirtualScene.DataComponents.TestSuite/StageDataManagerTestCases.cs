using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters;
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
        private Mock<IDataAdapter<IStage>> _dataAdapterMock;

        [SetUp]
        public void SetUp()
        {
            _stageMock = new Mock<IStage>();
            _stageName = Helper.GetUniqueName();
            _stageMock.SetupGet(p => p.Name).Returns(_stageName);
            var dataAdaptersPoolMock = new Mock<DataAdaptersPool>();
            _dataAdapterMock = new Mock<IDataAdapter<IStage>>();
            dataAdaptersPoolMock.Setup(m => m.GetFileSystemDataAdapter<IStage>()).Returns(_dataAdapterMock.Object);
            ServiceLocator.Set(dataAdaptersPoolMock.Object);
            _stageDataManager = new StageDataManager();
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
    }
}
