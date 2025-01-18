using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEditor;

namespace Unity.Services.LevelPlay.Editor
{
    class PackmanQueryEntry
    {
        internal readonly string packageName;
        internal readonly Action<bool> callback;

        internal PackmanQueryEntry(string packageName, Action<bool> callback)
        {
            this.packageName = packageName;
            this.callback = callback;
        }
    }

    class LevelPlayPackmanQuerier
    {
        readonly Queue<PackmanQueryEntry> queryEntries = new Queue<PackmanQueryEntry>();
        readonly object queueLock = new object();
        ListRequest packageListRequest;

        static LevelPlayPackmanQuerier s_Instance;
        public static LevelPlayPackmanQuerier instance
        {
            get
            {
                return s_Instance = s_Instance ?? new LevelPlayPackmanQuerier();
            }
        }

        internal void CheckIfPackageIsInstalledWithUpm(string packageName, Action<bool> callback)
        {
            lock (queueLock)
            {
                queryEntries.Enqueue(new PackmanQueryEntry(packageName, callback));

                if (packageListRequest == null)
                {
                    EditorApplication.update += PackageListRequestProgress;
                    packageListRequest = Client.List();
                }
            }
        }

        void PackageListRequestProgress()
        {
            if (!packageListRequest.IsCompleted || packageListRequest.Status != StatusCode.Success)
            {
                return;
            }

            lock (queueLock)
            {
                while (queryEntries.Count > 0)
                {
                    var query = queryEntries.Dequeue();
                    query.callback(ComparePackagesName(packageListRequest.Result, query.packageName));
                }

                EditorApplication.update -= PackageListRequestProgress;
                packageListRequest = null;
            }
        }

        static bool ComparePackagesName(PackageCollection packages, string packageName)
        {
            foreach (var package in packages)
            {
                if (string.CompareOrdinal(package.name, packageName) == 0
                    || package.dependencies.Any(dependency => string.CompareOrdinal(dependency.name, packageName) == 0))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
