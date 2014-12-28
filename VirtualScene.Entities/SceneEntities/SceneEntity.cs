using System;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.Entities.SceneEntities
{
    /// <summary>
    /// An entity in the scene
    /// </summary>
    public abstract class SceneEntity : ISceneEntity
    {
        private SceneElement _geometry;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneEntity" />
        /// </summary>
        protected SceneEntity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// The is of the <see cref="ISceneEntity" />
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Representation of the <see cref="ISceneEntity" /> in the scene.
        /// </summary>
        [XmlIgnore]
        public virtual SceneElement Geometry
        {
            get
            {
                return _geometry?? (_geometry = CreateGeometry());
            }
            set
            {
                _geometry = value;
            }
        }

        /// <summary>
        /// Build the <see cref="SceneElement" /> specific for each type of <see cref="ISceneEntity" />
        /// </summary>
        /// <returns>Returns <see cref="SceneElement" /></returns>
        protected abstract SceneElement CreateGeometry();

        /// <summary>
        /// The name of the entity in the scene
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            var sceneEntity = (SceneEntity)obj;
            return string.Equals(Name, sceneEntity.Name) 
                   && GeometryEqualityHelper.SceneElementEqual(Geometry, sceneEntity.Geometry);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Id.ToString().GetHashCode();
        }
    }
}