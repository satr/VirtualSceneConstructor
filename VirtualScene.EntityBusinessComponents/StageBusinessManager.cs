using VirtualScene.BusinessComponents.Common;
using VirtualScene.BusinessComponents.Core;
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
        /// <param name="stageName">The name of the stage. When stage's name is different than this name - the stage is saved with this new name.</param>
        /// <returns></returns>
        public ActionResult<IStage> Save(IStage stage, string stageName)
        {
            var actionResult = new ActionResult<IStage>(Resources.Title_Save_Stage);
            DataManager.Save(stage, stageName, actionResult);
            if (actionResult.Success)
                stage.Name = stageName;
            return actionResult;
        }

        /// <summary>
        /// The data manager to save/load stages
        /// </summary>
        public IStageDataManager DataManager {
            get { return ServiceLocator.Get<DataManagerPool>().GetStageDataManager(); } 
        }
    }
}
