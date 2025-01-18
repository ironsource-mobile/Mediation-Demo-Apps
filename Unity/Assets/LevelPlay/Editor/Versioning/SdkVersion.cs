using System;
using System.Collections.Generic;

namespace Unity.Services.LevelPlay.Editor
{
    internal sealed class SdkVersion : IEquatable<SdkVersion>
    {
        internal string Version { get; }
        internal string AndroidSdkVersion { get; }
        internal string IosSdkVersion { get; }
        internal string Message { get; }

        internal SdkVersion(string version, string androidSdkVersion, string iosSdkVersion, string message)
        {
            this.Version = version;
            this.AndroidSdkVersion = androidSdkVersion;
            this.IosSdkVersion = iosSdkVersion;
            this.Message = message;
        }

        internal SdkVersion(string version, Dictionary<string, object> jsonDictionary)
        {
            this.Version = version;
            this.AndroidSdkVersion = jsonDictionary["androidSdkVersion"] as string;
            this.IosSdkVersion = jsonDictionary["iosSdkVersion"] as string;
            this.Message = jsonDictionary["message"] as string;
        }

        public bool Equals(SdkVersion other)
        {
            if (other == null)
            {
                return false;
            }

            if (!Version.Equals(other.Version))
            {
                return false;
            }

            if (!AndroidSdkVersion.Equals(other.AndroidSdkVersion))
            {
                return false;
            }

            if (!IosSdkVersion.Equals(other.IosSdkVersion))
            {
                return false;
            }

            if (!Message.Equals(other.Message))
            {
                return false;
            }

            return true;
        }
    }
}
