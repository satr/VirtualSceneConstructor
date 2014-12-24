using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters;
using VirtualScene.Entities;

namespace VirtualScene.EntityDataComponents
{
    /// <summary>
    /// The data manager to save/load stages
    /// </summary>
    public class StageDataManager : IStageDataManager
    {
        private readonly IDataAdapter<IStage> _dataAdapter;

        /// <summary>
        /// Creates a new instance of the <see cref="StageDataManager"/>
        /// </summary>
        /// <param name="dataAdapter"></param>
        public StageDataManager(IDataAdapter<IStage> dataAdapter)
        {
            _dataAdapter = dataAdapter;
        }

        /// <summary>
        /// Save the stage with specified name.
        /// </summary>
        /// <param name="stage">The stage to be saved.</param>
        /// <returns>Result of the operation</returns>
        public IActionResult Save(IStage stage)
        {
            return _dataAdapter.Save(stage);
        }

        /// <summary>
        /// Load the stage by specified name.
        /// </summary>
        /// <param name="name">The name of the stage to load.</param>
        /// <returns></returns>
        public ActionResult<IStage> Load(string name)
        {
            return _dataAdapter.Load(name);
        }
    }
}
