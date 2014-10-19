namespace VirtualScene.EntityDataComponents
{
    /// <summary>
    /// The pool providing data-managers
    /// </summary>
    public class DataManagerPool
    {
        /// <summary>
        /// The data-manager for save/load operations on stages
        /// </summary>
        /// <returns></returns>
        public virtual IStageDataManager GetStageDataManager()
        {
            return new StageDataManager();
        }
    }
}
