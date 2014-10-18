using System.Collections.ObjectModel;
using VirtualScene.BusinessComponents.Core.Entities;

namespace VirtualScene.BusinessComponents.Core
{
    /// <summary>
    /// The stage with objects in 3D environment
    /// </summary>
    public class Stage : IStage
    {
        /// <summary>
        /// Creates a new stage
        /// </summary>
        public Stage()
        {
            Entities = new ObservableCollection<ISceneEntity>();
        }

        /// <summary>
        /// A list of visual representations of objects in the scene
        /// </summary>
        public ObservableCollection<ISceneEntity> Entities { get; set; }
    }
}