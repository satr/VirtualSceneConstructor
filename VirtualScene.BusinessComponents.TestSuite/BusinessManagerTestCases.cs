using Moq;
using NUnit.Framework;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerTestCases
    {
        private BusinessManager _businessManager;
        private Mock<ISceneEngine> _sceneEngineMock;

        [SetUp]
        public void Init()
        {
            _sceneEngineMock = new Mock<ISceneEngine>();
            _businessManager = new BusinessManager();
        }

        [Test]
        public void AddCubeToSceneTest()
        {
            _businessManager.AddSceneElementInSpace<Cube>(_sceneEngineMock.Object, 0, 0, 0);
            _sceneEngineMock.Verify(m => m.AddSceneElement(It.IsAny<Cube>()), Times.Once());
        }
    }
}