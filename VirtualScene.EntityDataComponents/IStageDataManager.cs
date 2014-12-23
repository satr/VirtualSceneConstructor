using VirtualScene.Common;
using VirtualScene.Entities;

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

        /// <summary>
        /// Load the stage by specified name.
        /// </summary>
        /// <param name="name">The name of the stage to load.</param>
        /// <returns></returns>
        ActionResult<IStage> Load(string name);
    }
}