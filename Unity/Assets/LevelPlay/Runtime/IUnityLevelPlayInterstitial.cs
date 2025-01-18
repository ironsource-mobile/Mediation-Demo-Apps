using System;

/// <summary>
/// Interface representing LevelPlay's interstitial events
/// </summary>
public interface IUnityLevelPlayInterstitial
{
    /// <summary>
    /// Event triggered when an interstitial ad failed to show
    /// </summary>
    event Action<IronSourceError, IronSourceAdInfo> OnAdShowFailed;

    /// <summary>
    /// Event triggered when an interstitial ad fails to load
    /// </summary>
    event Action<IronSourceError> OnAdLoadFailed;

    /// <summary>
    /// Event triggered when an interstitial ad is ready
    /// </summary>
    event Action<IronSourceAdInfo> OnAdReady;

    /// <summary>
    /// Event triggered when an interstitial ad is opened
    /// </summary>
    event Action<IronSourceAdInfo> OnAdOpened;

    /// <summary>
    /// Event triggered when an interstitial ad is closed
    /// </summary>
    event Action<IronSourceAdInfo> OnAdClosed;

    /// <summary>
    /// Event triggered when an interstitial ad showed successfully
    /// </summary>
    event Action<IronSourceAdInfo> OnAdShowSucceeded;

    /// <summary>
    /// Event triggered when an interstitial ad is clicked
    /// </summary>
    event Action<IronSourceAdInfo> OnAdClicked;
}
