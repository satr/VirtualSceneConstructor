using System.Collections.Specialized;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    public class BusinessManagerTestCases: BusinessManagerTestCasesBase
    {
    
        [Test]
        public void AddCubeToSceneTest()
        {
            const string testEntityName = "TestEntityName";
            BusinessManager.AddSceneElementInSpace(SceneContentMock.Object, GeometryPrimitiveFactory.CreateCube(), 0, 0, 0, testEntityName);
            AssertSceneEntityCollectionAction(NotifyCollectionChangedAction.Add);
        }
    }
}