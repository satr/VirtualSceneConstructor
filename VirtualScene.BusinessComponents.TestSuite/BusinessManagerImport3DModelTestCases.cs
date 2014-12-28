using System.Collections.Specialized;
using Moq;
using NUnit.Framework;
using SharpGL.SceneGraph.Core;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Importers;
using VirtualScene.BusinessComponents.Core.Pools;
using VirtualScene.Common;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerImport3DModelTestCases : BusinessManagerTestCasesBase
    {
        private Mock<GeometryImportersPool> _geometryImportersPoolMock;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _geometryImportersPoolMock = Helper.MockObjectInServiceLocator<GeometryImportersPool>();
            var importerMock = new Mock<IGeometryImporter>();
            _geometryImportersPoolMock.Setup(m => m.GetWavefrontFormatImporter()).Returns(importerMock.Object);
            var actionResultMock = new Mock<ActionResult<SceneElement>>();
            actionResultMock.SetupGet(p => p.Value).Returns(Mock.Of<SceneElement>());
            importerMock.Setup(m => m.LoadGeometry(It.IsAny<string>(), It.IsAny<ISceneEngine>())).Returns(actionResultMock.Object);
        }

        [Test]
        public void TestImport3DModel()
        {
            SceneContentBusinessManager.Import3DModel("filename", "filepath", SceneContentMock.Object);
            StageMock.Verify(m => m.Add(It.IsAny<ISceneEntity>()), Times.Once());
        }
    }
}