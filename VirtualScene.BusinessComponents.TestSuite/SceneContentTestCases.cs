using System.Collections.Generic;
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
        private IStage _stage;
        private IEnumerable<ISceneEntity> _receivedSelectedSceneElements;
        private bool _selectedSceneEntitiesEventFired;

        [SetUp]
        public void Init()
        {
            ClearSelectedSceneElementsResult();
            _sceneEngineMock = new Mock<ISceneEngine>();
            _stage = new Stage();
            _sceneContent = new SceneContent(_sceneEngineMock.Object);
            _sceneContent.SelectedSceneElementsChanged += (sender, entities) =>
            {
                _receivedSelectedSceneElements = entities;
                _selectedSceneEntitiesEventFired = true;
            };
        }

        private void ClearSelectedSceneElementsResult()
        {
            _receivedSelectedSceneElements = null;
            _selectedSceneEntitiesEventFired = false;
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
            _sceneContent.Stage = _stage;

            Assert.AreSame(_stage, _sceneContent.Stage);
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
            _sceneContent.Stage = _stage;
            _sceneContent.Stage = _stage;

            _sceneEngineMock.Verify(m => m.Clear(), Times.Once());
        }

        [Test]
        public void TestSceneEngineHandlesSceneEntityAddedToStage()
        {
            _sceneContent.Stage = _stage;

            var sceneEntity = Mock.Of<ISceneEntity>();
            _stage.Add(sceneEntity);

            _sceneEngineMock.Verify(m => m.AddSceneEntity(sceneEntity), Times.Once());
        }

        [Test]
        public void TestSceneEngineHandlesSceneEntityRemovedToStage()
        {
            var sceneEntity = Mock.Of<ISceneEntity>();
            _stage.Add(sceneEntity);
            _sceneContent.Stage = _stage;

            _stage.Remove(sceneEntity);

            _sceneEngineMock.Verify(m => m.RemoveSceneEntity(sceneEntity), Times.Once());
        }

        [Test]
        public void TestStageGetsNewEntityWhenItIsAddedToSceneContent()
        {
            _sceneContent.Stage = _stage;
            var sceneEntity = Mock.Of<ISceneEntity>();

            _sceneContent.Stage.Add(sceneEntity);

            CollectionAssert.Contains(_stage.Items, sceneEntity);
        }

        [Test]
        public void TestSetNewCollectionToSelectedItems()
        {
            const int sceneEntityCount = 2;
            var items = Helper.MockList<ISceneEntity>(sceneEntityCount);

            var origCollection = _sceneContent.SelectedItems;
            _sceneContent.SetSelectedItems(items);

            Assert.AreSame(origCollection, _sceneContent.SelectedItems);
            CollectionAssert.AreEqual(items, _sceneContent.SelectedItems);
        }

        [Test]
        public void TestSetEmptyCollectionToSelectedItems()
        {
            _sceneContent.SetSelectedItems(Helper.MockList<ISceneEntity>(2));

            _sceneContent.SetSelectedItems(new ObservableCollection<ISceneEntity>());

            CollectionAssert.IsEmpty(_sceneContent.SelectedItems);
        }

        [Test]
        public void TestSetNullToSelectionItems()
        {
            _sceneContent.SetSelectedItems(Helper.MockList<ISceneEntity>(1));

            _sceneContent.SetSelectedItems(null);

            Assert.IsNotNull(_sceneContent.SelectedItems);
            CollectionAssert.IsEmpty(_sceneContent.SelectedItems);
        }

        [Test]
        public void TestSelectedChangedEventOnSetItems()
        {
            var itemsToSelect = Helper.MockList<ISceneEntity>(2);
            ClearSelectedSceneElementsResult();

            _sceneContent.SetSelectedItems(itemsToSelect);

            Assert.IsTrue(_selectedSceneEntitiesEventFired);
            CollectionAssert.AreEqual(itemsToSelect, _receivedSelectedSceneElements);
        }

        [Test]
        public void TestSelectedChangedEventOnSetEmptyCollection()
        {
            ClearSelectedSceneElementsResult();

            _sceneContent.SetSelectedItems(new List<ISceneEntity>());

            Assert.IsTrue(_selectedSceneEntitiesEventFired);
            CollectionAssert.IsEmpty(_receivedSelectedSceneElements);
        }

        [Test]
        public void TestSelectedChangedEventOnSetNull()
        {
            ClearSelectedSceneElementsResult();

            _sceneContent.SetSelectedItems(null);

            Assert.IsTrue(_selectedSceneEntitiesEventFired);    
            CollectionAssert.IsEmpty(_receivedSelectedSceneElements);
        }

        [Test]
        public void TestSelectedChangedEventOnChangeStage()
        {
            _sceneContent.SetSelectedItems(Helper.MockList<ISceneEntity>(1));
            _stage.Add(Mock.Of<ISceneEntity>());
            ClearSelectedSceneElementsResult();

            _sceneContent.Stage = _stage;

            Assert.IsTrue(_selectedSceneEntitiesEventFired);    
            CollectionAssert.IsEmpty(_receivedSelectedSceneElements);
        }

        [Test]
        public void TestSelectedChangedWhenItemWhichIsSelected()
        {
            var stage = new Stage();
            var sceneEntity1 = Mock.Of<ISceneEntity>();
            stage.Add(sceneEntity1);
            var sceneEntity2 = Mock.Of<ISceneEntity>();
            stage.Add(sceneEntity2);
            _sceneContent.Stage = stage;
            _sceneContent.SetSelectedItems(new List<ISceneEntity> { sceneEntity1, sceneEntity2 });
            ClearSelectedSceneElementsResult();

            _sceneContent.Stage.Remove(sceneEntity1);

            Assert.IsTrue(_selectedSceneEntitiesEventFired);
            CollectionAssert.DoesNotContain(_sceneContent.SelectedItems, sceneEntity1);
            CollectionAssert.Contains(_sceneContent.SelectedItems, sceneEntity2);
        }
    }
}
