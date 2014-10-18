using Moq;
using VirtualScene.BusinessComponents.Common;

namespace VirtualScene.BusinessComponents.TestSuite
{
    internal class Helper
    {
        public static Mock<T> CreateMockInServiceLocator<T>() where T : class, new()
        {
            var mock = new Mock<T>();
            ServiceLocator.Set(mock.Object);
            return mock;
        }

        public static T CreateMockedObject<T>() where T : class
        {
            return new Mock<T>().Object;
        }
    }
}
