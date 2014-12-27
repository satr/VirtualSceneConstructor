using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Managers;
using VirtualScene.Common;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    public abstract class BusinessManagerTestCasesBase
    {
        protected Mock<ISceneContent> SceneContentMock;
        protected Mock<IStage> StageMock;
        protected SceneContentBusinessManager SceneContentBusinessManager;

        [SetUp]
        public virtual void Init()
        {
            SceneContentMock = new Mock<ISceneContent>();
            StageMock = new Mock<IStage>();
            SceneContentMock.SetupGet(m => m.Stage).Returns(StageMock.Object);
            SceneContentBusinessManager = new SceneContentBusinessManager();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }
    }
}