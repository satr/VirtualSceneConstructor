using NUnit.Framework;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;

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
            var sceneEntity = new SceneEntity {Geometry = new Cube()};
            _stage.Entities.Add(sceneEntity);

        }
    }
}