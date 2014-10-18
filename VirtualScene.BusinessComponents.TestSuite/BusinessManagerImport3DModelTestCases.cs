using Moq;
using NUnit.Framework;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Importers;
using VirtualScene.BusinessComponents.Core.Pools;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerImport3DModelTestCases
    {
        private BusinessManager _businessManager;
        private Mock<GeometryImportersPool> _geometryImportersPoolMock;
        private Mock<ISceneContent> _sceneContentMock;

        [SetUp]
        public void Init()
        {
            _sceneContentMock = new Mock<ISceneContent>();
            _geometryImportersPoolMock = Helper.CreateMockInServiceLocator<GeometryImportersPool>();
            var importerMock = new Mock<IGeometryImporter>();
            _geometryImportersPoolMock.Setup(m => m.GetWavefrontFormatImporter()).Returns(importerMock.Object);
            var actionResultMock = new Mock<ActionResult<SceneElement>>();
            actionResultMock.SetupGet(p => p.Value).Returns(Helper.CreateMockedObject<SceneElement>());
            importerMock.Setup(m => m.LoadGeometry(It.IsAny<string>(), It.IsAny<ISceneEngine>())).Returns(actionResultMock.Object);

            _businessManager = new BusinessManager();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }

        [Test]
        public void TestImport3DModel()
        {
            _businessManager.Import3DModel("filename", "filepath", _sceneContentMock.Object);

            _sceneContentMock.Verify(m => m.Add(It.IsAny<ISceneEntity>()), Times.Once());
        }
    }
}