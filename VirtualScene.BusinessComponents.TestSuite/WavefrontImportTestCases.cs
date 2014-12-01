using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using NUnit.Framework;
using SharpGL.SceneGraph.Assets;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Importers;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class WavefrontImportTestCases
    {
        private WavefrontFormatImporter _importer;
        private Mock<ISceneEngine> _sceneEngineMock;
        private List<Material> _materialsAssets;
        private Texture _texture;

        [SetUp]
        public void Init()
        {
            _sceneEngineMock = new Mock<ISceneEngine>();
            _materialsAssets = new List<Material>();
            _sceneEngineMock.Setup(m => m.GetAssets<Material>()).Returns(_materialsAssets);
            _texture = new Texture();
            _sceneEngineMock.Setup(m => m.LoadOrCreateTexture(It.IsAny<string>(), It.IsAny<string>())).Returns(_texture);

            _importer = new WavefrontFormatImporter();
        }

        [TearDown]
        public void TearDown()
        {
            ServiceLocator.Clear();
        }

        [Test]
        public void TestFailWhenFileNotExist()
        {
            var invalidFileName = Guid.NewGuid().ToString();
            var actionResult = _importer.LoadGeometry(invalidFileName, _sceneEngineMock.Object);

            Assert.IsFalse(actionResult.Success);
        }

        [Test]
        public void TestImportCube()
        {
            string geometryFileName = Mother.CreateWavefrontFormatFile();
            var actionResult = _importer.LoadGeometry(geometryFileName, _sceneEngineMock.Object);

            Assert.IsTrue(actionResult.Success);
            Assert.IsNotNull(actionResult.Value);
            _sceneEngineMock.Verify(m => m.AddAsset(It.IsAny<Material>()), Times.Never());
            Assert.AreEqual(1, actionResult.Warnings.Count);
        }

        [Test]
        public void TestImportCubeWithMaterials()
        {
            string geometryFileName = Mother.CreateWavefrontFormatFile();
            Mother.CreateMaterialsFile(geometryFileName);
            var actionResult = _importer.LoadGeometry(geometryFileName, _sceneEngineMock.Object);

            Assert.IsTrue(actionResult.Success);
            _sceneEngineMock.Verify(m => m.AddAsset(It.IsAny<Material>()), Times.Once());
        }
    }
}