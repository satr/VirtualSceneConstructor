using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters;
using VirtualScene.Entities;
using VirtualScene.EntityDataComponents;

namespace VirtualScene.EntityBusinessComponents
{
    /// <summary>
    /// The business manager for stage operations
    /// </summary>
    public class StageBusinessManager
    {
        private readonly StageDataManager _dataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StageBusinessManager"/>
        /// </summary>
        /// <param name="dataAdapter"></param>
        public StageBusinessManager(IDataAdapter<IStage> dataAdapter)
        {
            _dataManager = new StageDataManager(dataAdapter);
        }

        /// <summary>
        /// Save the stage with specified name.
        /// </summary>
        /// <param name="stage">The stage to be saved.</param>
        /// <returns></returns>
        public IActionResult Save(IStage stage)
        {
            return _dataManager.Save(stage);
        }

        /// <summary>
        /// Load the stage by specified name.
        /// </summary>
        /// <param name="name">The name of the stage to load.</param>
        /// <returns></returns>
        public ActionResult<IStage> Load(string name)
        {
            return _dataManager.Load(name);
        }
    }
}
