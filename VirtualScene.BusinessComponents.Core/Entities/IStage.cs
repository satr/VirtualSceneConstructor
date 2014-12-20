using System.Collections.ObjectModel;

namespace VirtualScene.BusinessComponents.Core.Entities
{
    /// <summary>
    /// The stage with objects in 3D environment
    /// </summary>
    public interface IStage
    {
        /// <summary>
        /// The name of the stage
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// A list of visual representations of objects in the scene
        /// </summary>
        ObservableCollection<ISceneEntity> Items { get; set; }
    }
}