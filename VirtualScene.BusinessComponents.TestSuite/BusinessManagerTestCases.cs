using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerTestCases: BusinessManagerTestCasesBase
    {
    
        [Test]
        public void AddCubeToSceneTest()
        {
            const string testEntityName = "TestEntityName";
            SceneContentBusinessManager.AddSceneElementInSpace(SceneContentMock.Object, GeometryPrimitiveFactory.CreateCube(), 0, 0, 0, testEntityName);
            StageMock.Verify(m => m.Add(It.IsAny<ISceneEntity>()), Times.Once());
        }
    }
}