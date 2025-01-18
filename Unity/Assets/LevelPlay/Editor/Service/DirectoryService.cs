using System;
using System.IO;

namespace Unity.Services.LevelPlay.Editor
{
    #nullable enable
    internal class DirectoryService : IDirectoryService
    {
        public bool Exists(string? path)
        {
            return Directory.Exists(path);
        }
    }
    #nullable disable
}
