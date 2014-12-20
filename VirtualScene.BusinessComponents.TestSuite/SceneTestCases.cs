using NUnit.Framework;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;

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
            _scene.SceneContainer.AddChild(GeometryPrimitiveFactory.CreateCube());
            Assert.AreEqual(1 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

        [Test]
        public void AddTwoCubesTest()
        {
            _scene.SceneContainer.AddChild(GeometryPrimitiveFactory.CreateCube());
            _scene.SceneContainer.AddChild(GeometryPrimitiveFactory.CreateCube());
            Assert.AreEqual(2 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

    }
}
