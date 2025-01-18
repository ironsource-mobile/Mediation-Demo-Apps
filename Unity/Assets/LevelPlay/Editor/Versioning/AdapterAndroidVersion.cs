using System;
using System.Collections.Generic;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class AdapterAndroidVersion : IEquatable<AdapterAndroidVersion>
    {
        internal string AdapterVersion { get; }
        internal string AdNetworkVersion { get; }

        internal AdapterAndroidVersion(string adapterVersion, string adNetworkVersion)
        {
            AdapterVersion = adapterVersion;
            AdNetworkVersion = adNetworkVersion;
        }

        internal AdapterAndroidVersion(Dictionary<string, object> jsonDictionary)
        {
            AdapterVersion = jsonDictionary["adapterVersion"] as string;
            AdNetworkVersion = jsonDictionary["adNetworkVersion"] as string;
        }

        public bool Equals(AdapterAndroidVersion other)
        {
            if (other == null)
            {
                return false;
            }

            if (!AdapterVersion.Equals(other.AdapterVersion))
            {
                return false;
            }

            if (!AdNetworkVersion.Equals(other.AdNetworkVersion))
            {
                return false;
            }

            return true;
        }
    }
}
