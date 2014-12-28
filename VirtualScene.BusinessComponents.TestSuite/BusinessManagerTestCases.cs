using Moq;
using NUnit.Framework;
using VirtualScene.Entities.SceneEntities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerTestCases: BusinessManagerTestCasesBase
    {
    
        [Test]
        public void AddCubeToSceneTest()
        {
            const string testEntityName = "TestEntityName";
            SceneContentBusinessManager.AddSceneElementInSpace<CubeEntity>(SceneContentMock.Object, 0, 0, 0, testEntityName);
            StageMock.Verify(m => m.Add(It.IsAny<ISceneEntity>()), Times.Once());
        }
    }
}