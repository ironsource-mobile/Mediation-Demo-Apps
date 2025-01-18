using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using UnityEditor;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class UnityPackage : IEquatable<UnityPackage>
    {
        internal string DisplayName { get; }
        internal string PackageURL { get; }
        internal Dictionary<string, UnityPackageVersion> Versions { get; }

        internal UnityPackage(string displayName, string packageURL, Dictionary<string, UnityPackageVersion> versions)
        {
            DisplayName = displayName;
            PackageURL = packageURL;
            Versions = versions;
        }

        internal UnityPackage(Dictionary<string, object> jsonDictionary)
        {
            DisplayName = jsonDictionary["displayName"] as string;
            PackageURL = jsonDictionary["packageURL"] as string;
            Versions = new Dictionary<string, UnityPackageVersion>();
            var versions = jsonDictionary["versions"] as Dictionary<string, object>;
            foreach (var version in versions)
            {
                Versions.Add(version.Key, new UnityPackageVersion(version.Key, version.Value as Dictionary<string, object>));
            }
        }

        public bool Equals(UnityPackage other)
        {
            if (other == null)
            {
                return false;
            }

            if (!DisplayName.Equals(other.DisplayName))
            {
                return false;
            }

            if (!PackageURL.Equals(other.PackageURL))
            {
                return false;
            }

            if (Versions.Count != other.Versions.Count)
            {
                return false;
            }

            foreach (var version in Versions)
            {
                if (!other.Versions.ContainsKey(version.Key))
                {
                    return false;
                }

                if (!version.Value.Equals(other.Versions[version.Key]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
