using System.Collections.Generic;
using System.Linq;
using Moq;
using VirtualScene.Common;

namespace VirtualScene.BusinessComponents.TestSuite
{
    internal class Helper
    {
        public static Mock<T> MockObjectInServiceLocator<T>() where T : class, new()
        {
            var mock = new Mock<T>();
            ServiceLocator.Set(mock.Object);
            return mock;
        }

        public static IList<T> MockList<T>(int count)
            where T: class
        {
            return Enumerable.Repeat(Mock.Of<T>(), count).ToList();
        }
    }
}
