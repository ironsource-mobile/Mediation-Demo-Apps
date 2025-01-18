using System;
using UnityEngine;

namespace com.unity3d.mediation
{
    class UnsupportedRewardedAd : IPlatformRewardedAd
    {
        public void Dispose()
        {
        }

        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo, LevelPlayReward> OnAdRewarded;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;
        public string AdUnitId { get; }

        public UnsupportedRewardedAd(string adUnitId)
        {
        }

        public void LoadAd()
        {
        }

        public void ShowAd(string placementName)
        {
        }

        public bool IsAdReady()
        {
            return false;
        }
    }
}
