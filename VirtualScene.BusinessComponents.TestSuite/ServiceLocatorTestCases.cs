using Moq;
using NUnit.Framework;
using VirtualScene.BusinessComponents.Common;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    class ServiceLocatorTestCases
    {
        [Test]
        public void TestCreateAndGetNotNullObject()
        {
            var testClassInstance = ServiceLocator.Get<TestClass>();
            Assert.IsNotNull(testClassInstance);
        }

        [Test]
        public void TestCreateAndGetObjectOfRequestedType()
        {
            var testClassInstance = ServiceLocator.Get<TestClass>();
            Assert.IsNotNull(testClassInstance);
            Assert.IsInstanceOf<TestClass>(testClassInstance);
        }

        [Test]
        public void TestGetSameInstanceTwice()
        {
            var instance1 = ServiceLocator.Get<TestClass>();
            var instance2 = ServiceLocator.Get<TestClass>();
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void TestClearInternalCash()
        {
            var instance1 = ServiceLocator.Get<TestClass>();
            ServiceLocator.Clear();
            var instance2 = ServiceLocator.Get<TestClass>();
            Assert.AreNotSame(instance1, instance2);
        }

        [Test]
        public void TestPresetInstanceIsNotNull()
        {
            ServiceLocator.Set(new TestClass());
            var presetInstance = ServiceLocator.Get<TestClass>();
            Assert.IsNotNull(presetInstance);
        }

        [Test]
        public void TestPresetInstanceIsSame()
        {
            var presetInstance = new TestClass();
            ServiceLocator.Set(presetInstance);
            var instance = ServiceLocator.Get<TestClass>();
            Assert.AreSame(presetInstance, instance);
        }

        [Test]
        public void TestPresetNullDoesNotThrowsException()
        {
            ServiceLocator.Set<TestClass>(null);
        }

        [Test]
        public void TestPresetInstanceReplacesExistingInstance()
        {
            var initialInstance = new TestClass();
            ServiceLocator.Set(initialInstance);
            var replacingInstance = new TestClass();
            ServiceLocator.Set(replacingInstance);

            var instance = ServiceLocator.Get<TestClass>();

            Assert.AreSame(replacingInstance, instance);
        }

        [Test]
        public void TestClearPresetInstance()
        {
            var presetInstance = new TestClass();
            ServiceLocator.Set(presetInstance);
            ServiceLocator.Clear();
            var instance = ServiceLocator.Get<TestClass>();
            Assert.AreNotSame(presetInstance, instance);
        }
    }

    internal class TestClass
    {
    }
}
