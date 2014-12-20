namespace VirtualScene.DataComponents.TestSuite
{
    public class TestEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var entity = (TestEntity)obj;
            return object.Equals(Id, entity.Id)
                   && string.Equals(Name, entity.Name);
        }

        public override int GetHashCode()
        {
            return string.Concat(Id.ToString(), Name).GetHashCode();
        }
    }
}