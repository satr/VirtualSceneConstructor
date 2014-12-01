using System.IO;
using VirtualScene.DataComponents.Common.Exceptions;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    /// <summary>
    /// File system data adapter
    /// </summary>
    public interface IFileSystemDataAdapter<T> : IDataAdapter<T>
    {
        /// <summary>
        /// The path to the folder with stages
        /// </summary>
        string StagesFolderPath { get; }

        /// <summary>
        /// Get folder list in the specified path. The folder is created when it does not exists.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns>The list of sub-folders contained in the specified folder.</returns>
        /// <exception cref="FileFoundWhenDirectoryExpectedException">Occures when file found instead of the specified folder.</exception>
        DirectoryInfo[] GetFolders(string path);
    }
}