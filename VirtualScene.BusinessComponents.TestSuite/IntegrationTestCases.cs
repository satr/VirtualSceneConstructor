using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestClass]
    public class IntegrationTestCases
    {
        private SceneContent _sceneContent;

        [TestInitialize]
        public void Init()
        {
            _sceneContent = new SceneContent();
        }

        [TestMethod]
        public void InitialStateTest()
        {
            Assert.IsNotNull(_sceneContent.SceneEngine);
        }


    }
}
