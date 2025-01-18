using System;
using UnityEngine;

namespace com.unity3d.mediation
{
    class UnsupportedInterstitialAd : IPlatformInterstitialAd
    {
        public void Dispose()
        {
        }

        public event Action<LevelPlayAdInfo> OnAdLoaded;
        public event Action<LevelPlayAdError> OnAdLoadFailed;
        public event Action<LevelPlayAdInfo> OnAdDisplayed;
        public event Action<LevelPlayAdInfo> OnAdClosed;
        public event Action<LevelPlayAdInfo> OnAdClicked;
        public event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event Action<LevelPlayAdInfo> OnAdInfoChanged;
        public string AdUnitId { get; }

        public UnsupportedInterstitialAd(string adUnitId)
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
