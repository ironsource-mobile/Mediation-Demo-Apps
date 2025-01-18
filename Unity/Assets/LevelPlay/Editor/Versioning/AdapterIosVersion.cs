using System;
using System.Collections.Generic;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class AdapterIosVersion : IEquatable<AdapterIosVersion>
    {
        internal string AdapterVersion { get; }
        internal string AdNetworkVersion { get; }

        internal AdapterIosVersion(string adapterVersion, string adNetworkVersion)
        {
            this.AdapterVersion = adapterVersion;
            this.AdNetworkVersion = adNetworkVersion;
        }

        internal AdapterIosVersion(Dictionary<string, object> jsonDictionary)
        {
            this.AdapterVersion = jsonDictionary["adapterVersion"] as string;
            this.AdNetworkVersion = jsonDictionary["adNetworkVersion"] as string;
        }

        public bool Equals(AdapterIosVersion other)
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
