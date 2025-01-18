using System;
using System.Threading.Tasks;
using UnityEngine;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Interface of a Banner Ad.
    /// </summary>
    interface IPlatformBannerAd : IDisposable
    {
        event EventHandler<LevelPlayAdInfo> OnAdLoaded;
        event EventHandler<LevelPlayAdError> OnAdLoadFailed;
        event EventHandler<LevelPlayAdInfo> OnAdClicked;
        event EventHandler<LevelPlayAdInfo> OnAdDisplayed;
        event EventHandler<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        event EventHandler<LevelPlayAdInfo> OnAdExpanded;
        event EventHandler<LevelPlayAdInfo> OnAdCollapsed;
        event EventHandler<LevelPlayAdInfo> OnAdLeftApplication;

        string AdUnitId { get; }
        LevelPlayAdSize AdSize { get; }
        string PlacementName { get; }
        LevelPlayBannerPosition Position { get; }

        void Load();
        void DestroyAd();
        void ShowAd();
        void HideAd();
        void PauseAutoRefresh();
        void ResumeAutoRefresh();
    }
}
