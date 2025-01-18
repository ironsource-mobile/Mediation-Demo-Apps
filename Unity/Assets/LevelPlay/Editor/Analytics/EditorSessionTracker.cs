using System.IO;
using UnityEditor;

namespace Unity.Services.LevelPlay.Editor.Analytics
{
    class EditorSessionTracker
    {
        const string k_NewSessionKey = "NewSession";
        const string k_LevelPlayPackageName = Constants.PackageName;

        internal static void NewSession()
        {
            if (!SessionState.GetBool(k_NewSessionKey, false))
            {
                SessionState.SetBool(k_NewSessionKey, true);
                LevelPlayPackmanQuerier.instance.CheckIfPackageIsInstalledWithUpm(k_LevelPlayPackageName, SendNewSessionEvent);
            }
        }

        static void SendNewSessionEvent(bool levelPlayIsUpm)
        {
#if !AdsMediation_BuilderProject
            EditorServices.Instance.EditorAnalyticsService.SendNewSession(levelPlayIsUpm
                ? EditorAnalyticsService.LevelPlayComponent.UpmPackage
                : EditorAnalyticsService.LevelPlayComponent.UnityPackage);
#endif
        }
    }
}
