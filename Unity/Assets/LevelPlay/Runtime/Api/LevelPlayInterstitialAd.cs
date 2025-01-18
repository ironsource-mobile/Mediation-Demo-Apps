using System;
using Unity.Services.LevelPlay;

namespace com.unity3d.mediation
{
    public class LevelPlayInterstitialAd : ILevelPlayInterstitialAd
    {
        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;

        readonly IPlatformInterstitialAd m_InterstitialAd;

        public string AdUnitId => m_InterstitialAd.AdUnitId;

        /// <summary>
        /// Creates and Initializes a new instance of the LevelPlay Interstitial Ad.
        /// </summary>
        /// <param name="adUnitId">The unique ID for the ad unit.</param>
        public LevelPlayInterstitialAd(string adUnitId)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            m_InterstitialAd = new AndroidInterstitialAd(adUnitId);
            #elif UNITY_IOS && !UNITY_EDITOR
            m_InterstitialAd = new IosInterstitialAd(adUnitId);
            #else
            m_InterstitialAd = new UnsupportedInterstitialAd(adUnitId);
            #endif

            m_InterstitialAd.OnAdLoaded += (info) => OnAdLoaded?.Invoke(info);
            m_InterstitialAd.OnAdLoadFailed += (error) => OnAdLoadFailed?.Invoke(error);
            m_InterstitialAd.OnAdDisplayed += (info) => OnAdDisplayed?.Invoke(info);
            m_InterstitialAd.OnAdClosed += (info) => OnAdClosed?.Invoke(info);
            m_InterstitialAd.OnAdClicked += (info) => OnAdClicked?.Invoke(info);
            m_InterstitialAd.OnAdDisplayFailed += (infoError) => OnAdDisplayFailed?.Invoke(infoError);
            m_InterstitialAd.OnAdInfoChanged += (info) => OnAdInfoChanged?.Invoke(info);
        }

        internal LevelPlayInterstitialAd(IPlatformInterstitialAd platformInterstitialAd)
        {
            m_InterstitialAd = platformInterstitialAd;
        }

        public void LoadAd()
        {
            m_InterstitialAd.LoadAd();
        }

        public void DestroyAd()
        {
            Dispose();
        }

        public void ShowAd(string placementName = null)
        {
            m_InterstitialAd.ShowAd(placementName);
        }

        public bool IsAdReady()
        {
            return m_InterstitialAd.IsAdReady();
        }

        /// <summary>
        /// Checks if a given Placement Name is capped.
        /// </summary>
        /// <param name="placementName">Placement Name for the Interstitial Ad.</param>
        /// <returns>Returns true if placement is capped, returns false if not.</returns>
        public static bool IsPlacementCapped(string placementName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return AndroidInterstitialAd.IsPlacementCapped(placementName);
#elif UNITY_IOS && !UNITY_EDITOR
            return IosInterstitialAd.IsPlacementCapped(placementName);
#else
            LevelPlayLogger.LogError("Unsupported platform: This API is not available on this platform.");
            return false;
#endif
        }

        public void Dispose()
        {
            m_InterstitialAd.Dispose();
        }
    }
}
