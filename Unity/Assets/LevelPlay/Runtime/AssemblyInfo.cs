using System.Runtime.CompilerServices;

// Test assemblies
#if UNITY_INCLUDE_TESTS
[assembly: InternalsVisibleTo("Unity.LevelPlay.Tests")]
[assembly: InternalsVisibleTo("Unity.LevelPlay.Editor.Tests")]
#endif

[assembly: InternalsVisibleTo("Unity.LevelPlay.Editor")]
