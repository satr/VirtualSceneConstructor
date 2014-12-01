using VirtualScene.BusinessComponents.Core;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters;

namespace VirtualScene.EntityDataComponents
{
    /// <summary>
    /// The data manager to save/load stages
    /// </summary>
    public class StageDataManager : IStageDataManager
    {

        private static IDataAdapter<IStage> DataAdapter
        {
            get { return ServiceLocator.Get<DataAdaptersPool>().GetFileSystemDataAdapter<IStage>(); }
        }

        /// <summary>
        /// Save the stage with specified name.
        /// </summary>
        /// <param name="stage">The stage to be saved.</param>
        /// <returns>Result of the operation</returns>
        public IActionResult Save(IStage stage)
        {
            return DataAdapter.Save(stage);
        }
    }
}
