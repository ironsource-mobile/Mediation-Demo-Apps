using System;

/// <summary>
/// Interface representing LevelPlay's manual rewarded video events
/// </summary>
public interface IUnityLevelPlayRewardedVideoManual
{
    /// <summary>
    /// Event triggered when a rewarded video becomes ready
    /// </summary>
    event Action<IronSourceAdInfo> OnAdReady;

    /// <summary>
    /// Event triggered when a rewarded video fails to load
    /// </summary>
    event Action<IronSourceError> OnAdLoadFailed;
}
