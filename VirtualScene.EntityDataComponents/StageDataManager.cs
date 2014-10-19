using VirtualScene.BusinessComponents.Core;

namespace VirtualScene.EntityDataComponents
{
    /// <summary>
    /// The data manager to save/load stages
    /// </summary>
    public class StageDataManager : IStageDataManager
    {
        /// <summary>
        /// Save the stage with specified name.
        /// </summary>
        /// <param name="stage">The stage to be saved.</param>
        /// <param name="stageName">The name of the stage.</param>
        /// <param name="actionResult">Result of the operation.</param>
        public void Save(IStage stage, string stageName, ActionResult<IStage> actionResult)
        {
            actionResult.AddError("Test fail1");
            actionResult.AddError("Test fail2");
            //TODO
        }
    }
}
