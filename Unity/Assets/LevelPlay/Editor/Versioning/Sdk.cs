using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class Sdk : IEquatable<Sdk>
    {
        internal string KeyName { get; }
        internal string DisplayName { get; }
        internal string DependencyXmlURL { get; }
        internal string DependencyXmlFileName { get; }
        internal Dictionary<string, SdkVersion> Versions { get; }

        internal Sdk(string keyName, string displayName, string dependencyXmlURL, string dependencyXmlFileName, Dictionary<string, SdkVersion> versions)
        {
            KeyName = keyName;
            DisplayName = displayName;
            DependencyXmlURL = dependencyXmlURL;
            DependencyXmlFileName = dependencyXmlFileName;
            Versions = versions;
        }

        internal Sdk(string keyName, Dictionary<string, object> jsonDictionary)
        {
            KeyName = keyName;
            DisplayName = jsonDictionary["displayName"] as string;
            DependencyXmlURL = jsonDictionary["dependencyXmlURL"] as string;
            DependencyXmlFileName = jsonDictionary["dependencyXmlFileName"] as string;
            Versions = new Dictionary<string, SdkVersion>();
            var versions = jsonDictionary["versions"] as Dictionary<string, object>;
            foreach (var version in versions)
            {
                Versions.Add(version.Key, new SdkVersion(version.Key, version.Value as Dictionary<string, object>));
            }
        }

        internal string GetDependencyXmlPath()
        {
            return Path.Combine("Assets", "LevelPlay", "Editor", DependencyXmlFileName);
        }

        public bool Equals(Sdk other)
        {
            if (other == null)
            {
                return false;
            }

            if (!KeyName.Equals(other.KeyName))
            {
                return false;
            }

            if (!DisplayName.Equals(other.DisplayName))
            {
                return false;
            }

            if (!DependencyXmlURL.Equals(other.DependencyXmlURL))
            {
                return false;
            }

            if (!DependencyXmlFileName.Equals(other.DependencyXmlFileName))
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
