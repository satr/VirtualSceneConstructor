using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestClass]
    public class BusinessManagerTestCases
    {
        private BusinessManager _businessManager;
        private static SceneFactory _sceneFactory;
        private Scene _scene;

        [ClassInitialize]
        public static void ClassFixture(TestContext testContext)
        {
            _sceneFactory = new SceneFactory();
        }

        [TestInitialize]
        public void Init()
        {
            _businessManager = new BusinessManager();
            _scene = _sceneFactory.Create();
        }

        [TestMethod]
        public void AddCumeToSceneTest()
        {
            _businessManager.AddSceneElementInSpace<Cube>(_scene, 0, 0, 0);
            Assert.AreEqual(1 + Constants.Scene.DefaultSceneElementsCount, _scene.SceneContainer.Children.Count);
            Assert.IsTrue(_scene.SceneContainer.Children.OfType<Cube>().Any());
        }
    }
}