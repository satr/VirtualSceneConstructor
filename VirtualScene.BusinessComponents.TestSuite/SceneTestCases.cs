using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Factories;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestClass]
    public class SceneTestCases
    {
        private Scene _scene;

        [TestInitialize]
        public void Init()
        {
            _scene = new SceneFactory().Create();
        }

        [TestMethod]
        public void InitialStateTests()
        {
            Assert.AreEqual(Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

        [TestMethod]
        public void AddCubeTest()
        {
            _scene.SceneContainer.AddChild(new Cube());
            Assert.AreEqual(1 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

        [TestMethod]
        public void AddTwoCubesTest()
        {
            _scene.SceneContainer.AddChild(new Cube());
            _scene.SceneContainer.AddChild(new Cube());
            Assert.AreEqual(2 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
        }

    }
}
