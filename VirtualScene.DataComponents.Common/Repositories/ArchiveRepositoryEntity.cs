namespace VirtualScene.DataComponents.Common.Repositories
{
    /// <summary>
    /// The repository based on the file-archive.
    /// </summary>
    public class ArchiveRepositoryEntity<T> : RepositoryEntity<T>
    {
        /// <summary>
        /// Creates a new instance of the archive-repository entry.
        /// </summary>
        /// <param name="entity"></param>
        public ArchiveRepositoryEntity(T entity) : base(entity)
        {
        }
    }
}