using System;
using Moq;
using VirtualScene.Common;
using VirtualScene.DataComponents.Common.DataAdapters.FileSystem;

namespace VirtualScene.UnitTesting.Common
{
    public class Helper
    {
        public static string GetUniqueName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
