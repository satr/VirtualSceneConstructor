using VirtualScene.DataComponents.Common.Properties;

namespace VirtualScene.DataComponents.Common.Exceptions
{
    /// <summary>
    /// Indicates thet file was found when directory was expected 
    /// </summary>
    public class FileFoundWhenDirectoryExpectedException: FileSystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFoundWhenDirectoryExpectedException" />
        /// </summary>
        /// <param name="path">The path to a folder</param>
        public FileFoundWhenDirectoryExpectedException(string path)
            : base(string.Format(Resources.Message_File_was_found_when_a_directory_was_expected_in_N, path))
        {
        }
    }
}
