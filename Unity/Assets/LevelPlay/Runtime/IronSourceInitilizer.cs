using System;
using Unity.Services.LevelPlay;
using UnityEngine;

[Obsolete("This class will be made private in a future version.", false)]
public class IronSourceInitilizer
{
#if UNITY_IOS || UNITY_ANDROID
    [RuntimeInitializeOnLoadMethod]
    static void Initilize()
    {
        var developerSettings = Resources.Load<IronSourceMediationSettings>(IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME);
        if (developerSettings != null)
        {
#if UNITY_ANDROID
            string appKey = developerSettings.AndroidAppKey;
#elif UNITY_IOS
            string appKey = developerSettings.IOSAppKey;
#endif
            if (developerSettings.EnableIronsourceSDKInitAPI == true)
            {
                if (appKey.Equals(string.Empty))
                {
                    LevelPlayLogger.LogWarning("IronSourceInitilizer Cannot init without AppKey");
                }
                else
                {
                    IronSource.UNITY_PLUGIN_VERSION += IronSource.UNITY_PLUGIN_VERSION.Contains("-r") ? "i" : "-i";
                    IronSource.Agent.init(appKey);
                }
            }

            if (developerSettings.EnableAdapterDebug)
            {
                IronSource.Agent.setAdaptersDebug(true);
            }

            if (developerSettings.EnableIntegrationHelper)
            {
                IronSource.Agent.validateIntegration();
            }
        }
    }

#endif
}
