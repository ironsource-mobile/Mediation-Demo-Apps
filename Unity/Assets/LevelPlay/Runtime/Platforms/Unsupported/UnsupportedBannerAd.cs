using System;
using Unity.Services.LevelPlay;

namespace com.unity3d.mediation
{
    public class UnsupportedBannerAd : IPlatformBannerAd
    {
        public UnsupportedBannerAd(string adUnitId, LevelPlayAdSize size, LevelPlayBannerPosition position, string placementId)
        {
            LevelPlayLogger.Log("UnsupportedBannerAd is not supported on this platform");
        }

        public event EventHandler<LevelPlayAdInfo> OnAdLoaded;
        public event EventHandler<LevelPlayAdError> OnAdLoadFailed;
        public event EventHandler<LevelPlayAdInfo> OnAdClicked;
        public event EventHandler<LevelPlayAdInfo> OnAdDisplayed;
        public event EventHandler<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        public event EventHandler<LevelPlayAdInfo> OnAdExpanded;
        public event EventHandler<LevelPlayAdInfo> OnAdCollapsed;
        public event EventHandler<LevelPlayAdInfo> OnAdLeftApplication;


        public LevelPlayBannerPosition Position { get; }

        public void Load()
        {
        }

        public void DestroyAd()
        {
        }

        public void ShowAd()
        {
        }

        public void HideAd()
        {
        }

        public void PauseAutoRefresh()
        {
        }

        public void ResumeAutoRefresh()
        {
        }

        public void SetAutoRefresh(bool flag)
        {
        }

        public void Dispose()
        {
        }

        public string AdUnitId { get; }
        public LevelPlayAdSize AdSize { get; }
        public LevelPlayAdSize Size { get; }
        public string PlacementName { get; }
    }
}
