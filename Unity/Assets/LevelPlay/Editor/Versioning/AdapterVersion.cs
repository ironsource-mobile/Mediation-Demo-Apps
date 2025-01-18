using System;
using System.Collections.Generic;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class AdapterVersion : IEquatable<AdapterVersion>
    {
        internal string Version { get; }
        internal string IronSourceSdkVersion { get; }
        internal AdapterAndroidVersion AndroidVersion { get; }
        internal AdapterIosVersion IosVersion { get; }

        internal AdapterVersion(string version, string ironSourceSdkVersion, AdapterAndroidVersion androidVersion, AdapterIosVersion iosVersion)
        {
            this.Version = version;
            this.IronSourceSdkVersion = ironSourceSdkVersion;
            this.AndroidVersion = androidVersion;
            this.IosVersion = iosVersion;
        }

        internal AdapterVersion(string version, Dictionary<string, object> jsonDictionary)
        {
            this.Version = version;
            this.IronSourceSdkVersion = jsonDictionary["ironSourceSdkVersion"] as string;
            if (jsonDictionary.ContainsKey("android"))
            {
                this.AndroidVersion = new AdapterAndroidVersion(jsonDictionary["android"] as Dictionary<string, object>);
            }
            if (jsonDictionary.ContainsKey("ios"))
            {
                this.IosVersion = new AdapterIosVersion(jsonDictionary["ios"] as Dictionary<string, object>);
            }
        }

        public bool Equals(AdapterVersion other)
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

            if (!AndroidVersion.Equals(other.AndroidVersion))
            {
                return false;
            }

            if (!IosVersion.Equals(other.IosVersion))
            {
                return false;
            }

            return true;
        }
    }
}
