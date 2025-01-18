using System;
using Unity.Services.LevelPlay;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Implements ILevelPlayRewardedAd to provide functionality for managing rewarded ads.
    /// </summary>
    public sealed class LevelPlayRewardedAd : ILevelPlayRewardedAd
    {
        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo, LevelPlayReward> OnAdRewarded;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;

        readonly IPlatformRewardedAd m_RewardedAd;

        public string AdUnitId => m_RewardedAd.AdUnitId;

        /// <summary>
        /// Creates and Initializes a new instance of the LevelPlay Rewarded Ad.
        /// </summary>
        /// <param name="adUnitId">The unique ID for the ad unit.</param>
        public LevelPlayRewardedAd(string adUnitId)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            m_RewardedAd = new AndroidRewardedAd(adUnitId);
            #elif UNITY_IOS && !UNITY_EDITOR
            m_RewardedAd = new IosRewardedAd(adUnitId);
            #else
            m_RewardedAd = new UnsupportedRewardedAd(adUnitId);
            #endif

            m_RewardedAd.OnAdLoaded += (info) => OnAdLoaded?.Invoke(info);
            m_RewardedAd.OnAdLoadFailed += (error) => OnAdLoadFailed?.Invoke(error);
            m_RewardedAd.OnAdDisplayed += (info) => OnAdDisplayed?.Invoke(info);
            m_RewardedAd.OnAdDisplayFailed += (infoError) => OnAdDisplayFailed?.Invoke(infoError);
            m_RewardedAd.OnAdRewarded += (info, reward) => OnAdRewarded?.Invoke(info, reward);
            m_RewardedAd.OnAdClicked += (info) => OnAdClicked?.Invoke(info);
            m_RewardedAd.OnAdClosed += (info) => OnAdClosed?.Invoke(info);
            m_RewardedAd.OnAdInfoChanged += (info) => OnAdInfoChanged?.Invoke(info);
        }

        internal LevelPlayRewardedAd(IPlatformRewardedAd platformRewardedAd)
        {
            m_RewardedAd = platformRewardedAd;
        }

        public void LoadAd()
        {
            m_RewardedAd.LoadAd();
        }

        public void ShowAd(string placementName = null)
        {
            m_RewardedAd.ShowAd(placementName);
        }

        public void DestroyAd()
        {
            Dispose();
        }

        public bool IsAdReady()
        {
            return m_RewardedAd.IsAdReady();
        }

        /// <summary>
        /// Checks if a given Placement Name is capped.
        /// </summary>
        /// <param name="placementName">Placement Name for the Rewarded Ad.</param>
        /// <returns>Returns true if placement is capped, returns false if not.</returns>
        public static bool IsPlacementCapped(string placementName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return AndroidRewardedAd.IsPlacementCapped(placementName);
#elif UNITY_IOS && !UNITY_EDITOR
            return IosRewardedAd.IsPlacementCapped(placementName);
#else
            LevelPlayLogger.LogError("Unsupported platform: This API is not available on this platform.");
            return false;
#endif
        }

        public void Dispose()
        {
            m_RewardedAd.Dispose();
        }
    }
}
