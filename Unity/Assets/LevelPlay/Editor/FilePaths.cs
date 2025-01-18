using System;
using System.IO;

namespace Unity.Services.LevelPlay.Editor
{
    internal static class FilePaths
    {
        internal static readonly string UnityPackageDirectoryPath = Path.Combine("Assets", "LevelPlay");
        internal static readonly string UpmPackageDirectoryPath = Path.Combine("Packages", "com.unity.services.levelplay");
        internal static readonly string LevelPlayVersionsJsonPath = Path.Combine("Assets", "LevelPlay", "Editor", "LevelPlayVersions.json");
        internal static readonly string LevelPlayPackageVersionsJsonPath = Path.Combine("Editor", "Json", "LevelPlayVersions.json");
        internal static readonly string ErrorIconPath = Path.Combine("Editor", "Icons", "Error", "Error.png");
        internal static readonly string InfoIconPath = Path.Combine("Editor", "Icons", "Info", "Info.png");
        internal static readonly string RecommendedIconPath = Path.Combine("Editor", "Icons", "Recommended", "Recommended.png");
        internal static readonly string NewIconPath = Path.Combine("Editor", "Icons", "New", "New.png");
        internal static readonly string DotUnityPackageFootprintFilePath = Path.Combine("Assets", "LevelPlay", "Editor", "LevelPlayFootprint.txt");
        internal static readonly string DotUnityPackageFootprintMetaFilePath = Path.Combine("Assets", "LevelPlay", "Editor", "LevelPlayFootprint.txt.meta");
    }
}
