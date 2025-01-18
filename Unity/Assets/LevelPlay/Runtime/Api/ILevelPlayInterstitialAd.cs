using System;

namespace com.unity3d.mediation
{
    /// <summary>
    /// APIs for LevelPlay Interstitial Ad in the Unity package.
    /// </summary>
    public interface ILevelPlayInterstitialAd : IDisposable
    {
        /// <summary>
        /// Invoked when the interstitial ad is loaded.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdLoaded;

        /// <summary>
        /// Invoked when the interstitial ad fails to load.
        /// </summary>
        event Action<LevelPlayAdError> OnAdLoadFailed;

        /// <summary>
        /// Invoked when the interstitial ad is displayed.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdDisplayed;

        /// <summary>
        /// Invoked when the interstitial ad is closed.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdClosed;

        /// <summary>
        /// Invoked when the interstitial ad when the user clicks on the ad.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdClicked;

        /// <summary>
        /// Invoked when the interstitial ad fails to display.
        /// </summary>
        event Action<LevelPlayAdDisplayInfoError> OnAdDisplayFailed;

        /// <summary>
        /// Invoked when the interstitial ad info is changed.
        /// </summary>
        event Action<LevelPlayAdInfo> OnAdInfoChanged;

        /// <summary>
        /// Gets the ad unit id of the ad.
        /// </summary>
        string AdUnitId { get; }

        /// <summary>
        /// Loads the Interstitial Ad.
        /// </summary>
        void LoadAd();

        /// <summary>
        /// Shows the Interstitial Ad.
        /// </summary>
        /// <param name="placementName"><i><b>(Optional)</b></i>Placement Name for the Interstitial Ad.</param>
        void ShowAd(string placementName = null);

        /// <summary>
        /// Destroys the Interstitial Ad.
        /// </summary>
        void DestroyAd();

        /// <summary>
        /// Checks if the interstitial ad is ready
        /// </summary>
        /// <returns>Returns true if the interstitial ad is ready, returns false if not.</returns>
        bool IsAdReady();
    }
}
