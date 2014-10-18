using NUnit.Framework;
using SharpGL.SceneGraph.Cameras;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class SceneEngineTestCases
    {
        private ISceneEngine _sceneEngine;

        [SetUp]
        public void Init()
        {
            _sceneEngine = new SceneEngine();
        }

        [Test]
        public void InitStateTest()
        {
            Assert.AreEqual(Constants.SceneEngine.DefaultUpdateRate, _sceneEngine.UpdateRate);
            Assert.IsNotNull(_sceneEngine.Cameras);
            Assert.AreEqual(1, _sceneEngine.Cameras.Count);
            Assert.AreEqual(typeof(LookAtCamera), _sceneEngine.Cameras[0].GetType());
        }

        [Test]
        public void SetCorrectUpdateRateTest()
        {
            const int newUpdateRate = Constants.SceneEngine.DefaultUpdateRate * 2;
            _sceneEngine.SetUpdateRate(newUpdateRate);
            Assert.AreEqual(newUpdateRate, _sceneEngine.UpdateRate);
        }

        [Test]
        public void SetInvalidNegativeUpdateRate()
        {
            const int negativeUpdateRate = -1 * Constants.SceneEngine.DefaultUpdateRate;
            _sceneEngine.SetUpdateRate(negativeUpdateRate);
            Assert.AreEqual(Constants.SceneEngine.DefaultUpdateRate, _sceneEngine.UpdateRate);
        }

        [Test]
        public void CreateViewportTest()
        {
            var sceneViewport = _sceneEngine.CreateViewport();
            Assert.IsNotNull(sceneViewport);
            Assert.IsNotNull(sceneViewport.Scene);
        }
    }
}
