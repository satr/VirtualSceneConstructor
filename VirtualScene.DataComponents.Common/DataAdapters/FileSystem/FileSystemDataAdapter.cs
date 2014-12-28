using System.IO;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Exceptions;
using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    /// <summary>
    /// Access to file-system files and folders
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class FileSystemDataAdapter<T> : IFileSystemDataAdapter<T>
    {
        private string _entityFolderPath;

        /// <summary>
        /// The name of the product root folder
        /// </summary>
        public const string ProductFilderName = "VirtualSceneConstructor";
        /// <summary>
        /// The name of the folder to keep stages
        /// </summary>
        public const string StagesFolderName = "Stages";

        /// <summary>
        /// The path to the folder where the entity data are stored.
        /// </summary>
        public string EntityFolderPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_entityFolderPath))
                    throw new DataAdapterConfigurationException(Resources.Message_Folder_path_for_the_entity_is_not_defined);
                return _entityFolderPath;
            }
            set { _entityFolderPath = value; }
        }

        /// <summary>
        /// Get folder list in the specified path. The folder is created when it does not exists.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns>The list of sub-folders contained in the specified folder.</returns>
        /// <exception cref="FileFoundWhenDirectoryExpectedException">Occures when file found instead of the specified folder.</exception>
        public DirectoryInfo[] GetFolders(string path)
        {
            CreateFolderIfDoesNotExist(path);
            return new DirectoryInfo(path).GetDirectories();
        }

        /// <summary>
        /// Build a folder if it does not exists
        /// </summary>
        /// <param name="path">The folder path</param>
        protected static void CreateFolderIfDoesNotExist(string path)
        {
            if (File.Exists(path))
                throw new FileFoundWhenDirectoryExpectedException(path);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Save an entity in the file system
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        public abstract IActionResult Save(T entity);

        /// <summary>
        /// Load the entity by its name.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <returns>The result of the operation with loaded entity in action-result property Value.</returns>
        public abstract ActionResult<T> Load(string name);
    }
}