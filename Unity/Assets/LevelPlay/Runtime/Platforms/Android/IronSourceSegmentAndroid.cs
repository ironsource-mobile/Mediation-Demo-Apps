using System;
using Unity.Services.LevelPlay;
using UnityEngine;

[Obsolete("Please use IronSourceEvents", false)]
public class IronSourceSegmentAndroid : AndroidJavaProxy, IUnitySegment
{
    public event Action<string> OnSegmentRecieved = delegate {};

    //implements UnitySegmentListener java interface
    public IronSourceSegmentAndroid() : base(IronSourceConstants.segmentBridgeListenerClass)
    {
        try
        {
            using (var pluginClass = new AndroidJavaClass(IronSourceConstants.bridgeClass))
            {
                var bridgeInstance = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
                bridgeInstance.Call("setUnitySegmentListener", this);
            }
        }
        catch (Exception e)
        {
            LevelPlayLogger.LogError("setUnitySegmentListener method doesn't exist, error: " + e.Message);
        }
    }

    public void onSegmentRecieved(string segmentName)
    {
        if (OnSegmentRecieved != null)
        {
            OnSegmentRecieved(segmentName);
        }
    }
}
