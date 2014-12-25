using System;

namespace VirtualScene.Entities
{
    /// <summary>
    /// Contains the information about type and assembly of an object.
    /// </summary>
    public class TypeInfo
    {
        /// <summary>
        /// Creates a new instance of the type information.
        /// </summary>
        public TypeInfo(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException(Properties.Resources.Message_TypeInfo_cannot_be_created_for_null);
            var type = obj.GetType();
            TypeName = type.ToString();
            ModuleName = type.Assembly.GetName().Name;
        }

        /// <summary>
        /// Creates a new instance of the type information. It is required during deserialiozation.
        /// </summary>
        private TypeInfo()
        {
        }

        /// <summary>
        /// The name of the module containing the type.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// The full name of the type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Creates a type corresponding to TypeName and ModuleName descriptions.
        /// </summary>
        /// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them.</param>
        /// <returns>The created type. null if the type cannot be created and throwOnError=false.</returns>
        public Type CreateEntityType(bool throwOnError)
        {
            return Type.GetType(string.Format("{0}, {1}", TypeName, ModuleName), throwOnError);
        }
    }
}
