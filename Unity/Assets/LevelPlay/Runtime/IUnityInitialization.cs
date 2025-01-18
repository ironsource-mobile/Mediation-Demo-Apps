using System;

/// <summary>
/// Interface to access initialization events
/// </summary>
public interface IUnityInitialization
{
    /// <summary>
    /// Event triggered when the SDK initialization completes successfully
    /// </summary>
    event Action OnSdkInitializationCompletedEvent;
}
