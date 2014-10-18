namespace VirtualScene.PresentationComponents.WPF.Models
{
    /// <summary>
    /// The model for importing a 3D model
    /// </summary>
    public class Import3DModelModel
    {
        /// <summary>
        /// Creates an new instance of the model
        /// </summary>
        public Import3DModelModel()
        {
            OperationCancelled = false;
        }

        /// <summary>
        /// The operation status. When  "false" - the operation has been cancelled
        /// </summary>
        public bool OperationCancelled { get; set; }

        /// <summary>
        /// The full name of the file
        /// </summary>
        public string FullFileName { get; set; }

        /// <summary>
        /// The name of the 3D entity
        /// </summary>
        public string Name { get; set; }
    }
}