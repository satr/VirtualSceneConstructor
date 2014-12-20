using NUnit.Framework;
using VirtualScene.DataComponents.Common.Repositories;

namespace VirtualScene.DataComponents.TestSuite
{
    [TestFixture]
    public class RepositoryEntityTestCases
    {
        private RepositoryEntity<TestEntity> _testRepositoryEntity;
        private TestEntity _testEntity;

        [SetUp]
        public void SetUp()
        {
            _testEntity = Mother.CreateTestEntity();
            _testRepositoryEntity = new RepositoryEntity<TestEntity>(_testEntity);
        }

        [Test]
        public void TestInitialState()
        {
            Assert.IsNotNull(_testRepositoryEntity.Entity);
            Assert.AreEqual(_testEntity.GetType(), _testRepositoryEntity.Entity.GetType());
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testRepositoryEntity.EntityType.TypeName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testRepositoryEntity.EntityType.ModuleName));
            Assert.AreEqual(_testEntity.GetType(), _testRepositoryEntity.EntityType.CreateEntityType(false));
            Assert.IsNotNull(_testRepositoryEntity.Items);
            Assert.AreEqual(0, _testRepositoryEntity.Items.Count);
        }

        [Test]
        public void TestAddEntity()
        {
            var testEntitySub1 = Mother.CreateTestEntity();
            _testRepositoryEntity.Items.Add(new RepositoryEntity<TestEntity>(testEntitySub1));

            Assert.AreEqual(1, _testRepositoryEntity.Items.Count);
            Assert.AreEqual(testEntitySub1, ((RepositoryEntity<TestEntity>)_testRepositoryEntity.Items[0]).Entity);
        }
    }
}
