#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using Unity.Services.LevelPlay;

[Obsolete("Please use IronSource.Agent", false)]
public class AndroidAgent : IronSourceIAgent
{
    private static AndroidJavaObject _androidBridge;
    private readonly static string AndroidBridge = "com.ironsource.unity.androidbridge.AndroidBridge";
    private const string REWARD_AMOUNT = "reward_amount";
    private const string REWARD_NAME = "reward_name";
    private const string PLACEMENT_NAME = "placement_name";

    const string WATERFALL_CONFIG_FLOOR = "floor";
    const string WATERFALL_CONFIG_CEILING = "ceiling";
    const string WATERFALL_CONFIG_API = "setWaterfallConfiguration";

    public AndroidAgent()
    {
        initEventsDispatcher();
    }

    #region IronSourceIAgent implementation
    public AndroidJavaObject getBridge()
    {
        if (_androidBridge == null)
            using (var pluginClass = new AndroidJavaClass(AndroidBridge))
                _androidBridge = pluginClass.CallStatic<AndroidJavaObject>("getInstance");

        return _androidBridge;
    }

    private void initEventsDispatcher()
    {
        IronSourceEventsDispatcher.initialize();
    }

    //******************* Base API *******************//

    /// <summary>
    /// Allows publishers to set configurations for a waterfall of a given ad type.
    /// </summary>
    /// <param name="waterfallConfiguration">The configuration for the given ad types waterfall. </param>
    /// <param name="adFormat">The AdFormat for which to configure the waterfall.</param>
    public void SetWaterfallConfiguration(WaterfallConfiguration waterfallConfiguration, AdFormat adFormat)
    {
        var ceiling = waterfallConfiguration.Ceiling;
        var floor = waterfallConfiguration.Floor;
        var dict = new Dictionary<string, string>();

        if (ceiling.HasValue)
        {
            dict.Add(WATERFALL_CONFIG_CEILING, ceiling.Value.ToString(CultureInfo.InvariantCulture));
        }

        if (floor.HasValue)
        {
            dict.Add(WATERFALL_CONFIG_FLOOR, floor.Value.ToString(CultureInfo.InvariantCulture));
        }

        var json = IronSourceJSON.Json.Serialize(dict);
        string stringAdFormat;

        switch (adFormat)
        {
            case AdFormat.RewardedVideo:
                stringAdFormat = "REWARDED_VIDEO";
                break;
            case AdFormat.Interstitial:
                stringAdFormat = "INTERSTITIAL";
                break;
            case AdFormat.Banner:
                stringAdFormat = "BANNER";
                break;
            default:
                return;
        }

        getBridge().Call(WATERFALL_CONFIG_API, json, stringAdFormat);
    }

    public void onApplicationPause(bool pause)
    {
        if (pause)
        {
            getBridge().Call("onPause");
        }
        else
        {
            getBridge().Call("onResume");
        }
    }

    public string getAdvertiserId()
    {
        return getBridge().Call<string>("getAdvertiserId");
    }

    public void validateIntegration()
    {
        getBridge().Call("validateIntegration");
    }

    public void shouldTrackNetworkState(bool track)
    {
        getBridge().Call("shouldTrackNetworkState", track);
    }

    public bool setDynamicUserId(string dynamicUserId)
    {
        return getBridge().Call<bool>("setDynamicUserId", dynamicUserId);
    }

    public void setAdaptersDebug(bool enabled)
    {
        getBridge().Call("setAdaptersDebug", enabled);
    }

    public void setMetaData(string key, string value)
    {
        getBridge().Call("setMetaData", key, value);
    }

    public void setMetaData(string key, params string[] values)
    {
        getBridge().Call("setMetaData", key, values);
    }

    public int? getConversionValue()
    {
        return null;
    }

    public void setManualLoadRewardedVideo(bool isOn)
    {
        getBridge().Call("setManualLoadRewardedVideo", isOn);
    }

    public void setNetworkData(string networkKey, string networkData)
    {
        getBridge().Call("setNetworkData", networkKey, networkData);
    }

    public void SetPauseGame(bool pause)
    {
    }

    //******************* SDK Init *******************//

    public void setUserId(string userId)
    {
        getBridge().Call("setUserId", userId);
    }

    public void init(string appKey)
    {
        getBridge().Call("setPluginData", "Unity", IronSource.pluginVersion(), IronSource.unityVersion());
        LevelPlayLogger.LogVerbose("IntegrationHelper pluginVersion: " + IronSource.pluginVersion());
        getBridge().Call("init", appKey);
    }

    public void init(string appKey, params string[] adUnits)
    {
        getBridge().Call("setPluginData", "Unity", IronSource.pluginVersion(), IronSource.unityVersion());
        LevelPlayLogger.LogVerbose("IntegrationHelper pluginVersion: " + IronSource.pluginVersion());
        getBridge().Call("init", appKey, adUnits);
    }

    //******************* RewardedVideo API *******************//

    public void loadRewardedVideo()
    {
        getBridge().Call("loadRewardedVideo");
    }

    public void showRewardedVideo()
    {
        getBridge().Call("showRewardedVideo");
    }

    public void showRewardedVideo(string placementName)
    {
        getBridge().Call("showRewardedVideo", placementName);
    }

    public bool isRewardedVideoAvailable()
    {
        return getBridge().Call<bool>("isRewardedVideoAvailable");
    }

    public bool isRewardedVideoPlacementCapped(string placementName)
    {
        return getBridge().Call<bool>("isRewardedVideoPlacementCapped", placementName);
    }

    public IronSourcePlacement getPlacementInfo(string placementName)
    {
        string placementInfo = getBridge().Call<string>("getPlacementInfo", placementName);
        IronSourcePlacement pInfo = null;
        if (placementInfo != null)
        {
            Dictionary<string, object> pInfoDic = IronSourceJSON.Json.Deserialize(placementInfo) as Dictionary<string, object>;
            string pName = pInfoDic[PLACEMENT_NAME].ToString();
            string rName = pInfoDic[REWARD_NAME].ToString();
            int rAmount = Convert.ToInt32(pInfoDic[REWARD_AMOUNT].ToString());

            pInfo = new IronSourcePlacement(pName, rName, rAmount);
        }
        return pInfo;
    }

    public void setRewardedVideoServerParams(Dictionary<string, string> parameters)
    {
        string json = IronSourceJSON.Json.Serialize(parameters);
        getBridge().Call("setRewardedVideoServerParams", json);
    }

    public void clearRewardedVideoServerParams()
    {
        getBridge().Call("clearRewardedVideoServerParams");
    }

    //******************* Interstitial API *******************//

    public void loadInterstitial()
    {
        getBridge().Call("loadInterstitial");
    }

    public void showInterstitial()
    {
        getBridge().Call("showInterstitial");
    }

    public void showInterstitial(string placementName)
    {
        getBridge().Call("showInterstitial", placementName);
    }

    public bool isInterstitialReady()
    {
        return getBridge().Call<bool>("isInterstitialReady");
    }

    public bool isInterstitialPlacementCapped(string placementName)
    {
        return getBridge().Call<bool>("isInterstitialPlacementCapped", placementName);
    }

    //******************* Banner API *******************//

    public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position)
    {
        loadBanner(size, position, "");
    }

    public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position, string placementName)
    {
        getBridge().Call("loadBanner", size.Description, (int)size.Width, (int)size.Height, (int)position, placementName, (bool)size.IsAdaptiveEnabled(), (bool)size.IsRespectAndroidCutoutsEnabled(), (float)size.getBannerContainerParams().Width, (float)size.getBannerContainerParams().Height);
    }

    public void destroyBanner()
    {
        getBridge().Call("destroyBanner");
    }

    public void displayBanner()
    {
        getBridge().Call("displayBanner");
    }

    public void hideBanner()
    {
        getBridge().Call("hideBanner");
    }

    public bool isBannerPlacementCapped(string placementName)
    {
        return getBridge().Call<bool>("isBannerPlacementCapped", placementName);
    }

    /// <summary>
    ///  Get the adaptive height according to the width.
    /// </summary>
    /// <param name="width"> The device width </param>
    public float getMaximalAdaptiveHeight(float width)
    {
        return getBridge().Call<float>("getMaximalAdaptiveHeight", width);
    }

    /// <summary>
    ///  Get device width.
    /// </summary>
    public float getDeviceScreenWidth()
    {
        return getBridge().Call<float>("getDeviceScreenWidth");
    }

    public void setSegment(IronSourceSegment segment)
    {
        Dictionary<string, string> dict = segment.getSegmentAsDict();
        string json = IronSourceJSON.Json.Serialize(dict);
        getBridge().Call("setSegment", json);
    }

    public void setConsent(bool consent)
    {
        getBridge().Call("setConsent", consent);
    }

    //******************* ConsentView API *******************//

    public void loadConsentViewWithType(string consentViewType)
    {
    }

    public void showConsentViewWithType(string consentViewType)
    {
    }

    //******************* ILRD API *******************//

    public void setAdRevenueData(string dataSource, Dictionary<string, string> impressionData)
    {
        string json = IronSourceJSON.Json.Serialize(impressionData);
        getBridge().Call("setAdRevenueData", dataSource, json);
    }

    //******************* TestSuite API *******************//

    public void launchTestSuite()
    {
        LevelPlayLogger.LogVerbose("AndroidAgent: launching TestSuite");
        getBridge().Call("launchTestSuite");
    }

    #endregion
}

#endif
