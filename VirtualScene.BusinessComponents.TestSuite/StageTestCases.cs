using NUnit.Framework;
using VirtualScene.Entities;
using VirtualScene.Entities.SceneEntities;

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
            var sceneEntity = new CubeEntity();
            _stage.Add(sceneEntity);

        }
    }
}