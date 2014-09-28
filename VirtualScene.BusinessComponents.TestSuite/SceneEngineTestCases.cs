using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestClass]
    public class SceneEngineTestCases
    {
        private SceneEngine _sceneEngine;

        [TestInitialize]
        public void Init()
        {
            _sceneEngine = new SceneEngine();
        }

        [TestMethod]
        public void InitStateTest()
        {
            Assert.IsNotNull(_sceneEngine.Scene);
            Assert.AreEqual(Constants.SceneEngine.DefaultUpdateRate, _sceneEngine.UpdateRate);
            Assert.IsNotNull(_sceneEngine.Cameras);
            Assert.AreEqual(1, _sceneEngine.Cameras.Count);
            Assert.AreEqual(typeof(ArcBallCamera), _sceneEngine.Cameras[0].GetType());
        }

        [TestMethod]
        public void SetCorrectUpdateRateTest()
        {
            const int newUpdateRate = Constants.SceneEngine.DefaultUpdateRate * 2;
            _sceneEngine.SetUpdateRate(newUpdateRate);
            Assert.AreEqual(newUpdateRate, _sceneEngine.UpdateRate);
        }

        [TestMethod]
        public void SetInvalidNegativeUpdateRate()
        {
            const int negativeUpdateRate = -1 * Constants.SceneEngine.DefaultUpdateRate;
            _sceneEngine.SetUpdateRate(negativeUpdateRate);
            Assert.AreEqual(Constants.SceneEngine.DefaultUpdateRate, _sceneEngine.UpdateRate);
        }

        [TestMethod]
        public void CreateViewportTest()
        {
            var sceneViewport = _sceneEngine.CreateViewport();
            Assert.IsNotNull(sceneViewport);
            Assert.IsNotNull(sceneViewport.Scene);
        }
    }
}
