using NUnit.Framework;
using SharpGL.SceneGraph;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Entities.SceneEntities.Factories;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class SceneTestCases
    {
        private Scene _scene;

        [SetUp]
        public void Init()
        {
            _scene = new SceneFactory().Create();
        }

        [Test]
        public void InitialStateTests()
        {
            Assert.AreEqual(Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

        [Test]
        public void AddCubeTest()
        {
            _scene.SceneContainer.AddChild(CubeFactory.Create(1));
            Assert.AreEqual(1 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

        [Test]
        public void AddTwoCubesTest()
        {
            _scene.SceneContainer.AddChild(CubeFactory.Create(1));
            _scene.SceneContainer.AddChild(CubeFactory.Create(1));
            Assert.AreEqual(2 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

    }
}
