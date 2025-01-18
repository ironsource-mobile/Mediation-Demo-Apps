using System;

/// <summary>
/// Interface representing LevelPlay's Rewarded video events
/// </summary>
public interface IUnityLevelPlayRewardedVideo
{
    /// <summary>
    /// Event triggered when a rewarded video fails to show
    /// </summary>
    event Action<IronSourceError, IronSourceAdInfo> OnAdShowFailed;

    /// <summary>
    /// Event triggered when a rewarded video is opened
    /// </summary>
    event Action<IronSourceAdInfo> OnAdOpened;

    /// <summary>
    /// Event triggered when a rewarded video is closed
    /// </summary>
    event Action<IronSourceAdInfo> OnAdClosed;

    /// <summary>
    /// Event triggered when a rewarded video is rewarded
    /// </summary>
    event Action<IronSourcePlacement, IronSourceAdInfo> OnAdRewarded;

    /// <summary>
    /// Event triggered when a rewarded video is clicked
    /// </summary>
    event Action<IronSourcePlacement, IronSourceAdInfo> OnAdClicked;

    /// <summary>
    /// Event triggered when a rewarded video becomes available
    /// </summary>
    event Action<IronSourceAdInfo> OnAdAvailable;

    /// <summary>
    /// Event triggered when a rewarded video becomes unavailable
    /// </summary>
    event Action OnAdUnavailable;
}
