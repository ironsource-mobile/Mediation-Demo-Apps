using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.LevelPlay;

#pragma warning disable 0618
/// <summary>
/// Access point to setup LevelPlay's configuration
/// </summary>
public class IronSourceConfig
{
    private const string unsupportedPlatformStr = "Unsupported Platform";
    private static IronSourceConfig _instance;

    /// <summary>
    /// Retrieve the singleton instance of LevelPlay's configuration
    /// </summary>
    public static IronSourceConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new IronSourceConfig();
            }
            return _instance;
        }
    }


    #if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaObject _androidBridge;
    private readonly static string AndroidBridge = "com.ironsource.unity.androidbridge.AndroidBridge";

    [Obsolete("This function will be made private in a future version. Please use IronSourceConfig.Instance.", false)]
    public IronSourceConfig()
    {
        using (var pluginClass = new AndroidJavaClass(AndroidBridge))
            _androidBridge = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
    }

    //Setters

    /// <summary>
    /// Sets the desired language
    /// </summary>
    /// <param name="language">Language to be used</param>
    [Obsolete("This function will be removed in a future version.", false)]
    public void setLanguage(string language)
    {
        _androidBridge.Call("setLanguage", language);
    }

    /// <summary>
    /// Enable or disable the client side callbacks
    /// </summary>
    /// <param name="status">Should the callbacks be enabled</param>
    [Obsolete("This function will be removed in a future version.", false)]
    public void setClientSideCallbacks(bool status)
    {
        _androidBridge.Call("setClientSideCallbacks", status);
    }

    /// <summary>
    /// Set custom parameters for a rewarded video
    /// </summary>
    /// <param name="rewardedVideoCustomParams">Dictionary of Key-value pairs of parameters</param>
    [Obsolete("This function will be removed in a future version. Please use IronSource.Agent.setRewardedVideoServerParams instead", false)]
    public void setRewardedVideoCustomParams(Dictionary<string, string> rewardedVideoCustomParams)
    {
        string json = IronSourceJSON.Json.Serialize(rewardedVideoCustomParams);
        _androidBridge.Call("setRewardedVideoCustomParams", json);
    }

    #elif (UNITY_IPHONE || UNITY_IOS) && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void CFSetLanguage(string language);

    [DllImport("__Internal")]
    private static extern void CFSetClientSideCallbacks(bool useClientSideCallbacks);

    [DllImport("__Internal")]
    private static extern void CFSetRewardedVideoCustomParams(string rewardedVideoCustomParams);


    /// <summary>
    /// Sets the desired language
    /// </summary>
    /// <param name="language">Language to be used</param>
    [Obsolete("This function will be removed in a future version.", false)]
    public void setLanguage(string language)
    {
        CFSetLanguage(language);
    }

    /// <summary>
    /// Enable or disable the client side callbacks
    /// </summary>
    /// <param name="status">Should the callbacks be enabled</param>
    [Obsolete("This function will be removed in a future version.", false)]
    public void setClientSideCallbacks(bool status)
    {
        CFSetClientSideCallbacks(status);
    }

    /// <summary>
    /// Set custom parameters for a rewarded video
    /// </summary>
    /// <param name="rewardedVideoCustomParams">Dictionary of Key-value pairs of parameters</param>
    [Obsolete("This function will be removed in a future version. Please use IronSource.Agent.setRewardedVideoServerParams instead", false)]
    public void setRewardedVideoCustomParams(Dictionary<string, string> rewardedVideoCustomParams)
    {
        string json = IronSourceJSON.Json.Serialize(rewardedVideoCustomParams);
        CFSetRewardedVideoCustomParams(json);
    }

    [Obsolete("This function will be made private in a future version. Please use IronSourceConfig.Instance.", false)]
    public IronSourceConfig()
    {
    }

    #else

    /// <summary>
    /// Sets the desired language
    /// </summary>
    /// <param name="language">Language to be used</param>
    [Obsolete("This function will be removed in a future version.", false)]
    public void setLanguage(string language)
    {
        LevelPlayLogger.Log(unsupportedPlatformStr);
    }

    /// <summary>
    /// Enable or disable the client side callbacks
    /// </summary>
    /// <param name="status">Should the callbacks be enabled</param>
    [Obsolete("This function will be removed in a future version.", false)]
    public void setClientSideCallbacks(bool status)
    {
        LevelPlayLogger.Log(unsupportedPlatformStr);
    }

    /// <summary>
    /// Set custom parameters for a rewarded video
    /// </summary>
    /// <param name="rewardedVideoCustomParams">Dictionary of Key-value pairs of parameters</param>
    [Obsolete("This function will be removed in a future version. Please use IronSource.Agent.setRewardedVideoServerParams instead", false)]
    public void setRewardedVideoCustomParams(Dictionary<string, string> rewardedVideoCustomParams)
    {
        LevelPlayLogger.Log(unsupportedPlatformStr);
    }

    [Obsolete("This function will be made private in a future version. Please use IronSourceConfig.Instance.", false)]
    public IronSourceConfig()
    {
        LevelPlayLogger.Log(unsupportedPlatformStr);
    }

    #endif
}