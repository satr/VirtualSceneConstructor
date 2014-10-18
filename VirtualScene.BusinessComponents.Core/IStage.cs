using System.Collections.ObjectModel;
using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The stage with objects in 3D environment
    /// </summary>
    public interface IStage
    {
        /// <summary>
        /// A list of visual representations of objects in the scene
        /// </summary>
        ObservableCollection<ISceneEntity> Entities { get; set; }
    }
}