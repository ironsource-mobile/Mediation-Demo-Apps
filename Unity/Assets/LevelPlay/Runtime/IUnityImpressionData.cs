using System;

/// <summary>
/// Interface to access impression data events
/// </summary>
public interface IUnityImpressionData
{
    /// <summary>
    /// Event triggered when an impression event occurs
    /// </summary>
    event Action<IronSourceImpressionData> OnImpressionDataReady;

    /// <summary>
    /// Event triggered when an impression event occurs
    /// </summary>
    event Action<IronSourceImpressionData> OnImpressionSuccess;
}
