#if LEVELPLAY_DEPENDENCIES_INSTALLED
using Newtonsoft.Json;

namespace Unity.Services.LevelPlay.Editor.IntegrationManager
{
    struct PackageVersionNativeCompatibility
    {
        [JsonProperty("PackageVersion")]
        public string PackageVersion { get; set; }

        [JsonProperty("CompatibleNativeVersionRange")]
        public string CompatibleNativeVersionRange { get; set; }
    }
}
#endif
