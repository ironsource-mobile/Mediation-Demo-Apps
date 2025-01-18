#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.unity3d.mediation
{
    [Obsolete("This class will be deprecated in version 9.0.0. Please use LevelPlaySdk instead.")]
    public class AndroidLevelPlaySdk : IUnityLevelPlayInitListener
    {
        private static AndroidJavaObject _levelPlayBridge;
        static readonly string LevelPlayBridge = "com.ironsource.unity.androidbridge.LevelPlayBridge";

        public static event Action<LevelPlayConfiguration> OnInitSuccess;
        public static event Action<LevelPlayInitError> OnInitFailed;
        private static IUnityLevelPlayInitListener _listener;


        public void onInitSuccess(string configuration)
        {
            OnInitSuccess?.Invoke(new LevelPlayConfiguration(configuration));
        }

        public void onInitFailed(string error)
        {
            OnInitFailed?.Invoke(new LevelPlayInitError(error));
        }

        private AndroidLevelPlaySdk()
        {
            _listener = new UnityLevelPlayInitListener(this);
        }

        static AndroidLevelPlaySdk()
        {
            IronSourceEventsDispatcher.initialize();
        }

        private static AndroidJavaObject GetBridge()
        {
            if (_levelPlayBridge == null)
                using (var pluginClass = new AndroidJavaClass(LevelPlayBridge))
                    _levelPlayBridge = pluginClass.CallStatic<AndroidJavaObject>("getInstance");

            return _levelPlayBridge;
        }

        public static void Initialize(string appKey, string userId, LevelPlayAdFormat[] adFormats)
        {
            GetBridge().Call("setPluginData", "Unity", IronSource.pluginVersion(), IronSource.unityVersion());
            _listener ??= new UnityLevelPlayInitListener(new AndroidLevelPlaySdk());
            GetBridge().Call("initialize", appKey, userId, GetAdFormatArray(adFormats), _listener);
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
                adFormatsArray[i] = adFormats[i].ToString();
            }
            return adFormatsArray;
        }
    }
}
#endif
