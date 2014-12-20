using System;
using System.IO;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.Exceptions;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    /// <summary>
    /// Access to file-system files and folders
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class FileSystemDataAdapter<T> : IFileSystemDataAdapter<T>
    {
        private readonly string _fileSystemCommonDocumentsFolderPath;

        /// <summary>
        /// The name of the product root folder
        /// </summary>
        public const string ProductFilderName = "VirtualSceneConstructor";
        /// <summary>
        /// The name of the folder to keep stages
        /// </summary>
        public const string StagesFolderName = "Stages";

        /// <summary>
        /// Initializes a new instance of the FileSystemDataAdapter
        /// </summary>
        protected FileSystemDataAdapter()
        {
            _fileSystemCommonDocumentsFolderPath = ServiceLocator.Get<FileSystemEnvironmentWrapper>().GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        }

        /// <summary>
        /// The path to the folder with stages
        /// </summary>
        public string StagesFolderPath
        {
            get
            {
                return Path.Combine(_fileSystemCommonDocumentsFolderPath, ProductFilderName, StagesFolderName);
            }
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
        /// Create a folder if it does not exists
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
    }
}