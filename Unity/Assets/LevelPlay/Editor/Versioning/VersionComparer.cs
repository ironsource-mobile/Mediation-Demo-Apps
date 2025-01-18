using System;
using System.Collections.Generic;

namespace Unity.Services.LevelPlay.Editor
{
    internal class VersionComparer : IComparer<string>
    {
        public int Compare(string version1, string version2)
        {
            var version1Parts = version1.Split('.');
            var version2Parts = version2.Split('.');

            for (int i = 0; i < version1Parts.Length; i++)
            {
                if (i >= version2Parts.Length)
                {
                    continue;
                }

                string version1Part = version1Parts[i];
                string version2Part = version2Parts[i];

                if (version1Part.Equals(version2Part))
                {
                    continue;
                }

                if (int.TryParse(version1Part, out var version1Int) && int.TryParse(version2Part, out var version2Int))
                {
                    return version1Int.CompareTo(version2Int);
                }

                return this.Limit(String.Compare(version1Part, version2Part, StringComparison.Ordinal));
            }

            return this.Limit(String.Compare(version1, version2, StringComparison.Ordinal));
        }

        private int Limit(int value)
        {
            if (value == 0)
            {
                return 0;
            }
            else if (value > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
