using Moq;
using NUnit.Framework;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerTestCases
    {
        private BusinessManager _businessManager;
        private Mock<ISceneContent> _sceneContentMock;

        [SetUp]
        public void Init()
        {
            _sceneContentMock = new Mock<ISceneContent>();
            _businessManager = new BusinessManager();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }

        [Test]
        public void AddCubeToSceneTest()
        {
            const string testEntityName = "TestEntityName";
            _businessManager.AddSceneElementInSpace<Cube>(_sceneContentMock.Object, 0, 0, 0, testEntityName);
            _sceneContentMock.Verify(m => m.Add(It.IsAny<ISceneEntity>()), Times.Once());
        }
    }
}