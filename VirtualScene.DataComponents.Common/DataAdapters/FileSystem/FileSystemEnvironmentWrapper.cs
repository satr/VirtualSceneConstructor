using System;

namespace VirtualScene.DataComponents.Common.DataAdapters.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemEnvironmentWrapper
    {
        /// <summary>
        /// Gives path to the system special folder 
        /// </summary>
        /// <param name="specialFolder"> An enumerated constant that identifies a system special folder.</param>
        /// <returns>The path to the folder</returns>
        public virtual string GetFolderPath(Environment.SpecialFolder specialFolder)
        {
            return Environment.GetFolderPath(specialFolder);
        }
    }
}