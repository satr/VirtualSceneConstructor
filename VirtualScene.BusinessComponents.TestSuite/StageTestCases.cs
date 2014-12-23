using NUnit.Framework;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class StageTestCases
    {
        private Stage _stage;

        [SetUp]
        public void SetUp()
        {
            _stage = new Stage();
        }

        [Test]
        public void TestAddEntity()
        {
            var sceneEntity = new SceneEntity { Geometry = GeometryPrimitiveFactory.CreateCube() };
            _stage.Items.Add(sceneEntity);

        }
    }
}