using VirtualScene.BusinessComponents.Core;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.Common;

namespace VirtualScene.EntityDataComponents
{
    /// <summary>
    /// The data manager to save/load stages
    /// </summary>
    public interface IStageDataManager
    {
        /// <summary>
        /// Save the stage with specified name.
        /// </summary>
        /// <param name="stage">The stage to be saved.</param>
        IActionResult Save(IStage stage);
    }
}