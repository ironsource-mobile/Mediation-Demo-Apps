using System;

namespace com.unity3d.mediation
{
    /// <summary>
    /// Defines the interface for banner ads in the LevelPlay mediation system.
    /// </summary>
    public interface ILevelPlayBannerAd: IDisposable
    {
        /// <summary>
        /// Loads the banner ad.
        /// </summary>
        void LoadAd();

        /// <summary>
        /// Destroys the banner ad and releases resources.
        /// </summary>
        void DestroyAd();

        /// <summary>
        /// Displays the banner ad to the user.
        /// </summary>
        void ShowAd();

        /// <summary>
        /// Hides the banner ad from the user.
        /// </summary>
        void HideAd();

        /// <summary>
        /// Gets the ad unit ID associated with this ad.
        /// </summary>
        string GetAdUnitId();

        /// <summary>
        /// Retrieves the size of the ad.
        /// </summary>
        LevelPlayAdSize GetAdSize();

        /// <summary>
        /// Retrieves the position of the banner ad.
        /// </summary>
        LevelPlayBannerPosition GetPosition();

        /// <summary>
        /// Retrieves the placement name associated with this ad.
        /// </summary>
        string GetPlacementName();

        /// <summary>
        /// Pauses the auto-refreshing of the banner ad.
        /// </summary>
        void PauseAutoRefresh();

        /// <summary>
        /// Resumes the auto-refreshing of the banner ad that was previously paused.
        /// </summary>
        void ResumeAutoRefresh();

        event Action<LevelPlayAdInfo> OnAdLoaded;
        event Action<LevelPlayAdError> OnAdLoadFailed;
        event Action<LevelPlayAdInfo> OnAdClicked;
        event Action<LevelPlayAdInfo> OnAdDisplayed;
        event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;
        event Action<LevelPlayAdInfo> OnAdExpanded;
        event Action<LevelPlayAdInfo> OnAdCollapsed;
        event Action<LevelPlayAdInfo> OnAdLeftApplication;
    }
}
