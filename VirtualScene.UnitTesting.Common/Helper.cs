using System;
using System.IO;
using Moq;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;

namespace VirtualScene.UnitTesting.Common
{
    public class Helper
    {
        private static int _uniqueIdCounter = 0;

        public static string GetUniqueName()
        {
            return Guid.NewGuid().ToString();
        }

        public static int GetUniqueInt()
        {
            return ++_uniqueIdCounter;
        }

        public static string CreateTempFolder()
        {
            var folderName = Guid.NewGuid().ToString();
            if (Directory.Exists(folderName))
                Directory.Delete(folderName, true);
            Directory.CreateDirectory(folderName);
            return folderName;
        }

        public static void MockFileSystemEnvironmentDocumentsFolder(string path)
        {
            var fileSystemEnvironmentWrapperMock = new Mock<FileSystemEnvironmentWrapper>();
            fileSystemEnvironmentWrapperMock.Setup(m => m.GetFolderPath(Environment.SpecialFolder.CommonDocuments)).Returns(path);
            ServiceLocator.Set(fileSystemEnvironmentWrapperMock.Object);
        }

    }
}
