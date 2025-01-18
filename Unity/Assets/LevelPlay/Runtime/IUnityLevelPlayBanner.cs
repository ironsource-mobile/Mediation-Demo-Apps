using System;

/// <summary>
/// Interface representing LevelPlay's banner events
/// </summary>
public interface IUnityLevelPlayBanner
{
    /// <summary>
    /// Event triggered when a banner is loaded
    /// </summary>
    event Action<IronSourceAdInfo> OnAdLoaded;

    /// <summary>
    /// Event triggered when a banner has left the application
    /// </summary>
    event Action<IronSourceAdInfo> OnAdLeftApplication;

    /// <summary>
    /// Event triggered when a banner ad screen has been dismissed
    /// </summary>
    event Action<IronSourceAdInfo> OnAdScreenDismissed;

    /// <summary>
    /// Event triggered when a banner ad screen has been presented
    /// </summary>
    event Action<IronSourceAdInfo> OnAdScreenPresented;

    /// <summary>
    /// Event triggered when a banner has been clicked
    /// </summary>
    event Action<IronSourceAdInfo> OnAdClicked;

    /// <summary>
    /// Event triggered when a banner has failed to load
    /// </summary>
    event Action<IronSourceError> OnAdLoadFailed;
}
