#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace com.unity3d.mediation
{
    [Obsolete("This class will be deprecated in version 9.0.0. Please use LevelPlaySdk instead.")]
    public class IosLevelPlaySdk : MonoBehaviour
    {
        public static event Action<LevelPlayConfiguration> OnInitSuccess;
        public static event Action<LevelPlayInitError> OnInitFailed;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        static IosLevelPlaySdk()
        {
        }

        public static void Initialize(string appKey, string userId, LevelPlayAdFormat[] adFormats)
        {
            setPluginData("Unity", IronSource.pluginVersion(), IronSource.unityVersion());
            new GameObject("IosLevelPlaySdk", typeof(IosLevelPlaySdk)).GetComponent<IosLevelPlaySdk>();
            LPMInitialize(appKey, userId, GetAdFormatArray(adFormats));
        }

        public static void SetPauseGame(bool pause)
        {
            LPMSetPauseGame(pause);
        }

        private static string[] GetAdFormatArray(LevelPlayAdFormat[] adFormats)
        {
            if (adFormats == null)
            {
                return null;
            }
            var adFormatsArray = new string[adFormats.Length];
            for (var i = 0; i < adFormats.Length; i++)
            {
                LevelPlayAdFormat adFormat = adFormats[i];
                var adFormatString = adFormat switch
                {
                    LevelPlayAdFormat.BANNER => "banner",
                    LevelPlayAdFormat.INTERSTITIAL => "interstitial",
                    LevelPlayAdFormat.REWARDED => "rewardedvideo",
                    _ => ""
                };
                adFormatsArray[i] = adFormatString;
            }
            return adFormatsArray;
        }

        [DllImport("__Internal")]
        private static extern void LPMInitialize(string appKey, string userId, string[] adFormats);

        [DllImport("__Internal")]
        private static extern void setPluginData(string pluginType, string pluginVersion, string pluginFrameworkVersion);

        [DllImport("__Internal")]
        private static extern void LPMSetPauseGame(bool pause);

        public void OnInitializationSuccess(string configuration)
        {
            OnInitSuccess?.Invoke(new LevelPlayConfiguration(configuration));
        }

        public void OnInitializationFailed(string error)
        {
            OnInitFailed?.Invoke(new LevelPlayInitError(error));
        }

    }

}
#endif
