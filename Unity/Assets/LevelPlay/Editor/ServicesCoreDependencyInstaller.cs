#if UNITY_2019_1_OR_NEWER && UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Unity.Services.LevelPlay.Editor
{
    static class ServicesCoreDependencyInstaller
    {
        const string k_CorePackageName = "com.unity.services.core";

        internal static void InstallServicesCoreIfNotFound()
        {
            if (Application.isBatchMode)
            {
                return;
            }
#if !LEVELPLAY_DEPENDENCIES_INSTALLED
            {
                LevelPlayPackmanQuerier.instance.CheckIfPackageIsInstalledWithUpm(k_CorePackageName, isInstalled =>
                {
                    if (!isInstalled)
                    {
                        Client.Add(k_CorePackageName);
                    }

                    AddScriptingSymbol();
                });
            }
#endif
        }

        static void AddScriptingSymbol()
        {
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,
                PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup) + ";" + "LEVELPLAY_DEPENDENCIES_INSTALLED");
        }
    }
}
#endif
