#if UNITY_2019_1_OR_NEWER && UNITY_EDITOR && LEVELPLAY_DEPENDENCIES_INSTALLED
using Unity.Services.Core.Editor;

namespace Unity.Services.LevelPlay.Editor
{
    /// <summary>
    /// Implementation of the <see cref="IEditorGameServiceIdentifier"/> for the LevelPlay package
    /// </summary>
    public struct LevelPlayIdentifier : IEditorGameServiceIdentifier
    {
        internal static string Key = Constants.PackageAnalyticsIdentifier;
        /// <summary>
        /// Gets the key for the User Reporting package
        /// </summary>
        /// <returns>The key for the service</returns>
        public string GetKey() => Key;
    }
}
#endif
