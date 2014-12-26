namespace VirtualScene.DataComponents.Common.Repositories
{
    /// <summary>
    /// The repository based on the file-archive.
    /// </summary>
    public class ArchiveRepositoryEntity<T> : RepositoryEntity<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveRepositoryEntity&lt;T&gt;" />
        /// </summary>
        /// <param name="entity"></param>
        public ArchiveRepositoryEntity(T entity) : base(entity)
        {
        }
    }
}