using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Common.Helpers;
using VirtualScene.Entities.Properties;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// The <see cref="ISceneEntity" /> with a <see cref="Cylinder" /> geometry.
    /// </summary>
    public class CylinderEntity : SceneEntity<Cylinder>
    {
        private float _topRadius;
        private float _baseRadius;
        private float _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="CylinderEntity" />
        /// </summary>
        public CylinderEntity()
            : base(Resources.Title_Cylinder)
        {
        }

        /// <summary>
        /// The top radius of the cylinder.
        /// </summary>
        public float TopRadius
        {
            get { return _topRadius; }
            set { Math.AssignValue(ref _topRadius, value, ConcreteGeometry, v => ConcreteGeometry.TopRadius = v, 0); }
        }

        /// <summary>
        /// The base radius of the cylinder.
        /// </summary>
        public float BaseRadius
        {
            get { return _baseRadius; }
            set { Math.AssignValue(ref _baseRadius, value, ConcreteGeometry, v => ConcreteGeometry.BaseRadius = v, 0); }
        }

        /// <summary>
        /// The height of the cylinder.
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { Math.AssignValue(ref _height, value, ConcreteGeometry, v => ConcreteGeometry.Height = v, 0); }
        }

        /// <summary>
        /// Update private data after new geometry was assigned.
        /// </summary>
        /// <param name="sceneElement">The concrete geometry of type <see cref="Cylinder" />.</param>
        protected override void UpdateFields(Cylinder sceneElement)
        {
            _topRadius = (float)(sceneElement == null ? 0 : sceneElement.TopRadius);
            _baseRadius = (float)(sceneElement == null ? 0 : sceneElement.BaseRadius);
            _height = (float)(sceneElement == null ? 0 : sceneElement.Height);
        }

        /// <summary>
        /// Build the <see cref="Cylinder" />.
        /// </summary>
        /// <returns>Returns the <see cref="Cylinder" /> as <see cref="SceneElement" />.</returns>
        protected override Cylinder CreateGeometry()
        {
            return new Cylinder {TopRadius = 1, BaseRadius = 1, Height = 1};
        }
    }
}