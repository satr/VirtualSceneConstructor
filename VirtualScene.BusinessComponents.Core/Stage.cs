using System.Collections.ObjectModel;
using System.Xml.Serialization;
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
            Items = new ObservableCollection<ISceneEntity>();
        }

        /// <summary>
        /// The name of the stage
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of visual representations of objects in the scene
        /// </summary>
        [XmlIgnore]
        public ObservableCollection<ISceneEntity> Items { get; set; }

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
            var stage = (Stage) obj;
            if (!string.Equals(stage.Name, Name) 
                || stage.Items.Count != Items.Count)
                return false;
// ReSharper disable LoopCanBeConvertedToQuery
            for (int i = 0; i < Items.Count; i++)
// ReSharper restore LoopCanBeConvertedToQuery
            {
                if (!stage.Items[i].Equals(Items[i]))
                    return false;
            }
            return true;
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
            return Name.GetHashCode();
        }
    }
}