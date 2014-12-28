using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="Disk" /> geometry.
    /// </summary>
    public class DiskEntity : SceneEntity<Disk>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskEntity" />
        /// </summary>
        public DiskEntity()
            : base(Resources.Title_Disk)
        {
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="Disk" />.</param>
        protected override void UpdateFields(Disk sceneElement)
        {
        }

        /// <summary>
        /// Build the <see cref="Disk" />.
        /// </summary>
        /// <returns>Returns the <see cref="Disk" /> as <see cref="SceneElement" />.</returns>
        protected override Disk CreateGeometry()
        {
            return new Disk();
        }
    }
}