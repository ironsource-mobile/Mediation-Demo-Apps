using System;

namespace Unity.Services.LevelPlay.Editor
{
    internal interface IFileService
    {
        void Delete(string path);
        #nullable enable
        bool Exists(string? path);
        #nullable disable
        void WriteAllBytes(string path, byte[] bytes);
        void Copy(string sourceFileName, string destFileName, bool overwrite);
        string ReadAllText(string path);
        #nullable enable
        void WriteAllText(string path, string? contents);
        #nullable disable
        void ImportPackage(string packagePath, bool interactive);
        string GetNewTempFilePath();
        string GetPathRelativeToLevelPlayPackage(string path);
        void CreateDirectoryForFilePath(string path);
    }
}
