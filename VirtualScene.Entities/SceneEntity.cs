using System;
using System.Xml.Serialization;
using SharpGL.SceneGraph.Core;

namespace VirtualScene.Entities
{
    /// <summary>
    /// An entity in the scene
    /// </summary>
    public class SceneEntity : ISceneEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneEntity" />
        /// </summary>
        public SceneEntity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// The is of the <see cref="ISceneEntity" />
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Visual representation of the entity in the scene
        /// </summary>
        [XmlIgnore]
        public SceneElement Geometry { get; set; }

        /// <summary>
        /// The name of the entity in the scene
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the <see cref="ISceneEntity" />
        /// </summary>
        public string Description
        {
            get { return Geometry == null ? "?" : Geometry.GetType().ToString(); }
        }

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