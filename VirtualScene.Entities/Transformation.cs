using SharpGL.SceneGraph.Transformations;

namespace VirtualScene.Entities
{
    public class Transformation
    {
        /// <summary>
        /// Gets or sets the x component of the translation.
        /// </summary>
        /// <value>
        /// The x component of the translation.
        /// </value>
        public float TranslateX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y component of the translation.
        /// </summary>
        /// <value>
        /// The y component of the translation.
        /// </value>
        public float TranslateY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the z component of the translation.
        /// </summary>
        /// <value>
        /// The z component of the translation.
        /// </value>
        public float TranslateZ
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the x component of the rotation.
        /// </summary>
        /// <value>
        /// The x component of the rotation.
        /// </value>
        public float RotateX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y component of the rotation.
        /// </summary>
        /// <value>
        /// The y component of the rotation.
        /// </value>
        public float RotateY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the z component of the rotation.
        /// </summary>
        /// <value>
        /// The z component of the rotation.
        /// </value>
        public float RotateZ
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the x component of the scale.
        /// </summary>
        /// <value>
        /// The x component of the scale.
        /// </value>
        public float ScaleX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y component of the scale.
        /// </summary>
        /// <value>
        /// The y component of the scale.
        /// </value>
        public float ScaleY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the z component of the scale.
        /// </summary>
        /// <value>
        /// The z component of the scale.
        /// </value>
        public float ScaleZ
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the transformation order.
        /// </summary>
        /// <value>
        /// The transformation order.
        /// </value>
        public LinearTransformationOrder TransformationOrder { get; set; }

        public void Transform(LinearTransformation linearTransformation)
        {
            linearTransformation.TranslateX = TranslateX;
            linearTransformation.TranslateY = TranslateY;
            linearTransformation.TranslateZ = TranslateZ;
            linearTransformation.RotateX = RotateX;
            linearTransformation.RotateY = RotateY;
            linearTransformation.RotateZ = RotateZ;
            linearTransformation.ScaleX = ScaleX;
            linearTransformation.ScaleY = ScaleY;
            linearTransformation.ScaleZ = ScaleZ;
            linearTransformation.TransformationOrder = TransformationOrder;
        }

        public static Transformation CreateForm(LinearTransformation linearTransformation)
        {
            return new Transformation
            {
                TranslateX = linearTransformation.TranslateX,
                TranslateY = linearTransformation.TranslateY,
                TranslateZ = linearTransformation.TranslateZ,
                RotateX = linearTransformation.RotateX,
                RotateY = linearTransformation.RotateY,
                RotateZ = linearTransformation.RotateZ,
                ScaleX = linearTransformation.ScaleX,
                ScaleY = linearTransformation.ScaleY,
                ScaleZ = linearTransformation.ScaleZ,
                TransformationOrder = linearTransformation.TransformationOrder
            };
        }
    }
}
