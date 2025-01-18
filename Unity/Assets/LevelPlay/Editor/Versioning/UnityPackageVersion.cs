using System;
using System.Collections.Generic;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class UnityPackageVersion : IEquatable<UnityPackageVersion>
    {
        internal string Version { get; }
        internal string IronSourceSdkVersion { get; }

        internal UnityPackageVersion(string version, string ironSourceSdkVersion)
        {
            Version = version;
            IronSourceSdkVersion = ironSourceSdkVersion;
        }

        internal UnityPackageVersion(string version, Dictionary<string, object> jsonDictionary)
        {
            Version = version;
            IronSourceSdkVersion = jsonDictionary["ironSourceSdkVersion"] as string;
        }

        public bool Equals(UnityPackageVersion other)
        {
            if (other == null)
            {
                return false;
            }

            if (!Version.Equals(other.Version))
            {
                return false;
            }

            if (!IronSourceSdkVersion.Equals(other.IronSourceSdkVersion))
            {
                return false;
            }

            return true;
        }
    }
}
