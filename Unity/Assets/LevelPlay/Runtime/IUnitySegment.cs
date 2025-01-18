using System;

/// <summary>
/// Interface representing LevelPlay's user segment
/// </summary>
public interface IUnitySegment
{
    /// <summary>
    /// Event triggered when a segment is received
    /// </summary>
    event Action<String> OnSegmentRecieved;
}
