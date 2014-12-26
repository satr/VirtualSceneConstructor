using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Common;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class SceneContentTestCases
    {
        private ISceneContent _sceneContent;
        private Mock<ISceneEngine> _sceneEngineMock;
        private Mock<SceneEngineFactory> _sceneEngineFactoryMock;
        private IStage _stage;
        private ObservableCollection<ISceneEntity> _sceneEntities;

        [SetUp]
        public void Init()
        {
            _sceneEngineFactoryMock = Helper.CreateMockInServiceLocator<SceneEngineFactory>();
            _sceneEngineMock = new Mock<ISceneEngine>();
            _sceneEngineFactoryMock.Setup(m => m.Create()).Returns(_sceneEngineMock.Object);
            var stageMock = new Mock<IStage>();
            _sceneEntities = new ObservableCollection<ISceneEntity>();
            stageMock.SetupGet(p => p.Items).Returns(_sceneEntities);
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

        [Test]
        public void TestSceneEngineHandlesSceneEntityAddedToStage()
        {
            _sceneContent.Stage = _stage;

            var sceneEntity = new Mock<ISceneEntity>().Object;
            _stage.Items.Add(sceneEntity);

            _sceneEngineMock.Verify(m => m.AddSceneEntity(sceneEntity), Times.Once());
        }

        [Test]
        public void TestSceneEngineHandlesSceneEntityRemovedToStage()
        {
            var sceneEntity = new Mock<ISceneEntity>().Object;
            _stage.Items.Add(sceneEntity);
            _sceneContent.Stage = _stage;

            _stage.Items.Remove(sceneEntity);

            _sceneEngineMock.Verify(m => m.RemoveSceneEntity(sceneEntity), Times.Once());
        }

        [Test]
        public void TestStageGetsNewEntityWhenItIsAddedToSceneContent()
        {
            _sceneContent.Stage = _stage;
            var sceneEntity = new Mock<ISceneEntity>().Object;

            _sceneContent.Stage.Items.Add(sceneEntity);

            CollectionAssert.Contains(_sceneEntities, sceneEntity);
        }

        [Test]
        public void TestAddSelectedItem()
        {
            var sceneEntity = Mock.Of<ISceneEntity>();
            _sceneContent.SelectedItems.Add(sceneEntity);

            CollectionAssert.Contains(_sceneContent.SelectedItems, sceneEntity);
        }

        [Test]
        public void TestRemoveSelectedItem()
        {
            var sceneEntity = Mock.Of<ISceneEntity>();
            _sceneContent.SelectedItems.Add(sceneEntity);

            _sceneContent.SelectedItems.Remove(sceneEntity);

            Assert.AreEqual(0, _sceneContent.SelectedItems.Count);
        }

        [Test]
        public void TestSetNewCollectionToSelectedItems()
        {
            var items = new ObservableCollection<ISceneEntity>
            {
                Mock.Of<ISceneEntity>(),
                Mock.Of<ISceneEntity>()
            };

            var origCollection = _sceneContent.SelectedItems;
            _sceneContent.SetSelectedItems(items);

            Assert.AreSame(origCollection, _sceneContent.SelectedItems);
            Assert.AreEqual(2, _sceneContent.SelectedItems.Count);
        }

        [Test]
        public void TestSetEmptyCollectionToSelectedItems()
        {
            _sceneContent.SelectedItems.Add(Mock.Of<ISceneEntity>());
            _sceneContent.SelectedItems.Add(Mock.Of<ISceneEntity>());

            _sceneContent.SetSelectedItems(new ObservableCollection<ISceneEntity>());

            Assert.AreEqual(0, _sceneContent.SelectedItems.Count);
        }

        [Test]
        public void TestSetNullToSelectionItems()
        {
            _sceneContent.SelectedItems.Add(Mock.Of<ISceneEntity>());

            _sceneContent.SetSelectedItems(null);

            Assert.IsNotNull(_sceneContent.SelectedItems);
            Assert.AreEqual(0, _sceneContent.SelectedItems.Count);
            
        }
    }
}
