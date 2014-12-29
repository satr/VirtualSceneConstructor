using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Common.Helpers;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="Sphere" /> geometry.
    /// </summary>
    public class SphereEntity : SceneEntity<Sphere>
    {
        private float _radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="SphereEntity" />
        /// </summary>
        public SphereEntity()
            : base(Resources.Title_Sphere)
        {
        }

        /// <summary>
        /// The radius of the <see cref="Sphere" />
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { Math.AssignValue(ref _radius, value, SceneElement, v => SceneElement.Radius = v, 0); }
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="Sphere" />.</param>
        protected override void UpdateFields(Sphere sceneElement)
        {
            _radius = (float)(sceneElement == null ? 0 : sceneElement.Radius);
        }

        /// <summary>
        /// Build the <see cref="Sphere" />.
        /// </summary>
        /// <returns>Returns the <see cref="Sphere" /> as <see cref="SceneElement" />.</returns>
        protected override Sphere CreateGeometry()
        {
            return new Sphere{Radius = 1};
        }
    }
}