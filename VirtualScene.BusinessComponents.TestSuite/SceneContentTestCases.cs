using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class SceneContentTestCases
    {
        private ISceneContent _sceneContent;
        private Mock<ISceneEngine> _sceneEngineMock;
        private Mock<SceneEngineFactory> _sceneEngineFactoryMock;
        private IStage _stage;

        [SetUp]
        public void Init()
        {
            _sceneEngineFactoryMock = Helper.CreateMockInServiceLocator<SceneEngineFactory>();
            _sceneEngineMock = new Mock<ISceneEngine>();
            _sceneEngineFactoryMock.Setup(m => m.Create()).Returns(_sceneEngineMock.Object);
            var stageMock = new Mock<IStage>();
            stageMock.SetupGet(p => p.Entities).Returns(new ObservableCollection<ISceneEntity>());
            _stage = stageMock.Object;
            _sceneContent = new SceneContent();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }

        [Test]
        public void TestInitialState()
        {
            Assert.IsNotNull(_sceneContent.SceneEngine);
            Assert.IsNull(_sceneContent.Stage);
        }
        
        [Test]
        public void TestSetStage()
        {
            var stage = _stage;
            _sceneContent.Stage = stage;

            Assert.AreSame(stage, _sceneContent.Stage);
        }

        [Test]
        public void TestCleanSceneEngineOnSettingStage()
        {
            _sceneContent.Stage = _stage;

            _sceneEngineMock.Verify(m => m.Clear(), Times.Once());
        }

        [Test]
        public void TestDoNotCleanSceneEngineOnSettingSameStage()
        {
            var stage = _stage;
            _sceneContent.Stage = stage;
            _sceneContent.Stage = stage;

            _sceneEngineMock.Verify(m => m.Clear(), Times.Once());
        }


    }
}
