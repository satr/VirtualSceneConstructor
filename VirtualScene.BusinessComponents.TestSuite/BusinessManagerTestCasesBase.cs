using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    public abstract class BusinessManagerTestCasesBase
    {
        protected Mock<ISceneContent> SceneContentMock;
        protected Mock<IStage> StageMock;
        protected ObservableCollection<ISceneEntity> SceneEntityCollection;
        protected NotifyCollectionChangedAction? SceneEntityCollectionAction;
        protected BusinessManager BusinessManager;

        [SetUp]
        public virtual void Init()
        {
            SceneContentMock = new Mock<ISceneContent>();
            StageMock = new Mock<IStage>();
            SceneContentMock.SetupGet(m => m.Stage).Returns(StageMock.Object);
            SceneEntityCollection = new ObservableCollection<ISceneEntity>();
            SceneEntityCollection.CollectionChanged += (sender, args) => SceneEntityCollectionAction = args.Action;
            SceneEntityCollectionAction = null;
            StageMock.SetupGet(m => m.Items).Returns(SceneEntityCollection);
            BusinessManager = new BusinessManager();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }

        protected void AssertSceneEntityCollectionAction(NotifyCollectionChangedAction expected)
        {
            Assert.IsNotNull(SceneEntityCollectionAction);
            Assert.AreEqual(expected, SceneEntityCollectionAction);
        }
    }
}