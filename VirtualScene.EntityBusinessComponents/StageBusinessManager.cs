using VirtualScene.BusinessComponents.Core;
using VirtualScene.Common;
using VirtualScene.EntityBusinessComponents.Properties;
using VirtualScene.EntityDataComponents;

namespace VirtualScene.EntityBusinessComponents
{
    /// <summary>
    /// The business manager for stage operations
    /// </summary>
    public class StageBusinessManager
    {
        /// <summary>
        /// Save the stage with specified name.
        /// </summary>
        /// <param name="stage">The stage to be saved.</param>
        /// <returns></returns>
        public IActionResult Save(IStage stage)
        {
            return DataManager.Save(stage);
        }

        /// <summary>
        /// The data manager to save/load stages
        /// </summary>
        public IStageDataManager DataManager {
            get { return ServiceLocator.Get<DataManagerPool>().GetStageDataManager(); } 
        }
    }
}
