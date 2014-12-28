using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="Disk" /> geometry.
    /// </summary>
    public class DiskEntity : SceneEntity
    {
        private readonly string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiskEntity" />
        /// </summary>
        public DiskEntity()
        {
            _description = Resources.Title_Disk;
        }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public override string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Build the <see cref="Disk" />.
        /// </summary>
        /// <returns>Returns the <see cref="Disk" /> as <see cref="SceneElement" />.</returns>
        protected override SceneElement CreateGeometry()
        {
            return new Disk();
        }
    }
}