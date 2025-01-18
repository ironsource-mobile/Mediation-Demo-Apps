using System;

namespace com.unity3d.mediation
{
    /// <summary>
    /// APIs for LevelPlay Rewarded Ad in the Unity package.
    /// </summary>
    public interface ILevelPlayRewardedAd : IDisposable
    {
        /// <summary>
        /// Invoked when the Rewarded ad is loaded.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdLoaded;

        /// <summary>
        /// Invoked when the Rewarded ad fails to load.
        /// </summary>
        event Action<LevelPlayAdError> OnAdLoadFailed;

        /// <summary>
        /// Invoked when the Rewarded ad is displayed.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdDisplayed;

        /// <summary>
        /// Invoked when the Rewarded ad fails to display.
        /// </summary>
        event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;

        /// <summary>
        /// Invoked when the Rewarded ad receives reward.
        /// </summary>
        event Action<LevelPlayAdInfo, LevelPlayReward> OnAdRewarded;

        /// <summary>
        /// Invoked when the Rewarded ad when the user clicks on the ad.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdClicked;

        /// <summary>
        /// Invoked when the Rewarded ad is closed.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdClosed;

        /// <summary>
        /// Invoked when the Rewarded ad info is changed.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdInfoChanged;

        /// <summary>
        /// Gets the ad unit id of the ad.
        /// </summary>
        string AdUnitId { get; }

        /// <summary>
        /// Loads the Rewarded Ad.
        /// </summary>
        void LoadAd();

        /// <summary>
        /// Shows the Rewarded Ad.
        /// </summary>
        /// <param name="placementName"><i><b>(Optional)</b></i>Placement Name for the Rewarded Ad.</param>
        void ShowAd(string placementName = null);

        /// <summary>
        /// Destroys the Rewarded Ad.
        /// </summary>
        void DestroyAd();

        /// <summary>
        /// Checks if the Rewarded ad is ready
        /// </summary>
        /// <returns>Returns true if the Rewarded ad is ready, returns false if not.</returns>
        bool IsAdReady();
    }
}
