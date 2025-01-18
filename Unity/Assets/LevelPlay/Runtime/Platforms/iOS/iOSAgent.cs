#if UNITY_IPHONE || UNITY_IOS
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System;
using System.Globalization;
using Unity.Services.LevelPlay;

[Obsolete("Please use IronSource.Agent", false)]
public class iOSAgent : IronSourceIAgent
{
    struct IOSWaterfallConfiguration
    {
        public double Floor;
        public double Ceiling;
    }

    [DllImport("__Internal")]
    private static extern void LPPSetWaterfallConfiguration(IOSWaterfallConfiguration configurationParams, AdFormat adFormat);

    [DllImport("__Internal")]
    private static extern void CFSetPluginData(string pluginType, string pluginVersion, string pluginFrameworkVersion);

    [DllImport("__Internal")]
    private static extern string CFGetAdvertiserId();

    [DllImport("__Internal")]
    private static extern void CFValidateIntegration();

    [DllImport("__Internal")]
    private static extern void CFShouldTrackNetworkState(bool track);

    [DllImport("__Internal")]
    private static extern bool CFSetDynamicUserId(string dynamicUserId);

    [DllImport("__Internal")]
    private static extern void CFSetAdaptersDebug(bool enabled);

    [DllImport("__Internal")]
    private static extern void CFSetMetaData(string key, string value);

    [DllImport("__Internal")]
    private static extern void CFSetMetaDataWithValues(string key, params string[] values);

    [DllImport("__Internal")]
    private static extern string CFGetConversionValue();

    [DllImport("__Internal")]
    private static extern void CFSetManualLoadRewardedVideo(bool isOn);

    [DllImport("__Internal")]
    private static extern void CFSetNetworkData(string networkKey, string networkData);

    delegate void ISUnityPauseGame(bool pause);
    [DllImport("__Internal")]
    private static extern void RegisterPauseGameFunction(bool pasue);


    //******************* SDK Init *******************//

    [DllImport("__Internal")]
    private static extern void CFSetUserId(string userId);

    [DllImport("__Internal")]
    private static extern void CFInit(string appKey);

    [DllImport("__Internal")]
    private static extern void CFInitWithAdUnits(string appKey, params string[] adUnits);

    //******************* RewardedVideo API *******************//

    [DllImport("__Internal")]
    private static extern void CFLoadRewardedVideo();

    [DllImport("__Internal")]
    private static extern void CFShowRewardedVideo();

    [DllImport("__Internal")]
    private static extern void CFShowRewardedVideoWithPlacementName(string placementName);

    [DllImport("__Internal")]
    private static extern bool CFIsRewardedVideoAvailable();

    [DllImport("__Internal")]
    private static extern bool CFIsRewardedVideoPlacementCapped(string placementName);

    [DllImport("__Internal")]
    private static extern string CFGetPlacementInfo(string placementName);

    [DllImport("__Internal")]
    private static extern void CFSetRewardedVideoServerParameters(string jsonString);

    [DllImport("__Internal")]
    private static extern void CFClearRewardedVideoServerParameters();

    //******************* Interstitial API *******************//

    [DllImport("__Internal")]
    private static extern void CFLoadInterstitial();

    [DllImport("__Internal")]
    private static extern void CFShowInterstitial();

    [DllImport("__Internal")]
    private static extern void CFShowInterstitialWithPlacementName(string placementName);

    [DllImport("__Internal")]
    private static extern bool CFIsInterstitialReady();

    [DllImport("__Internal")]
    private static extern bool CFIsInterstitialPlacementCapped(string placementName);

    //******************* Banner API *******************//

    [DllImport("__Internal")]
    private static extern void CFLoadBanner(string description, int width, int height, int position, string placementName, bool isAdaptive, float containerWidth, float containerHeight);

    [DllImport("__Internal")]
    private static extern void CFDestroyBanner();

    [DllImport("__Internal")]
    private static extern void CFDisplayBanner();

    [DllImport("__Internal")]
    private static extern void CFHideBanner();

    [DllImport("__Internal")]
    private static extern bool CFIsBannerPlacementCapped(string placementName);

    [DllImport("__Internal")]
    private static extern float CFIGetMaximalAdaptiveHeight(float width);

    [DllImport("__Internal")]
    private static extern float CFIGetDeviceScreenWidth();

    //******************* Segment And Consent *******************//

    [DllImport("__Internal")]
    private static extern void CFSetSegment(string json);

    [DllImport("__Internal")]
    private static extern void CFSetConsent(bool consent);

    //******************* ConsentView API *******************//

    [DllImport("__Internal")]
    private static extern void CFLoadConsentViewWithType(string consentViewType);

    [DllImport("__Internal")]
    private static extern void CFShowConsentViewWithType(string consentViewType);

    //******************* ILRD API *******************//

    [DllImport("__Internal")]
    private static extern void CFSetAdRevenueData(string dataSource, string impressionData);

    //******************* TestSuite API *******************//

    [DllImport("__Internal")]
    private static extern void CFLaunchTestSuite();

    public iOSAgent()
    {
    }

    #region IronSourceIAgent implementation

    //******************* Base API *******************//

    /// <summary>
    /// Allows publishers to set configurations for a waterfall of a given ad type.
    /// If the floor and/or the ceiling are null, the <b>IOSWaterfallConfiguration</b> passed values will be 0.0.
    /// </summary>
    /// <param name="adFormat">The AdFormat for which to configure the waterfall.</param>
    /// <param name="waterfallConfiguration">The configuration for the given ad types waterfall. </param>
    public void SetWaterfallConfiguration(WaterfallConfiguration waterfallConfiguration, AdFormat adFormat)
    {
        var config = new IOSWaterfallConfiguration
        {
            Floor = waterfallConfiguration.Floor ?? 0.0,
            Ceiling = waterfallConfiguration.Ceiling ?? 0.0
        };

        LPPSetWaterfallConfiguration(config, adFormat);
    }

    public void onApplicationPause(bool pause)
    {
    }

    public string getAdvertiserId()
    {
        return CFGetAdvertiserId();
    }

    public void validateIntegration()
    {
        CFValidateIntegration();
    }

    public void shouldTrackNetworkState(bool track)
    {
        CFShouldTrackNetworkState(track);
    }

    public bool setDynamicUserId(string dynamicUserId)
    {
        return CFSetDynamicUserId(dynamicUserId);
    }

    public void setAdaptersDebug(bool enabled)
    {
        CFSetAdaptersDebug(enabled);
    }

    public void setMetaData(string key, params string[] values)
    {
        CFSetMetaDataWithValues(key, values);
    }

    public void setMetaData(string key, string value)
    {
        CFSetMetaData(key, value);
    }

    public int? getConversionValue()
    {
        CultureInfo invCulture = CultureInfo.InvariantCulture;
        int parsedInt;
        if (int.TryParse(string.Format(invCulture, "{0}", CFGetConversionValue()), NumberStyles.Any, invCulture, out parsedInt))
        {
            return parsedInt;
        }

        return null;
    }

    public void setManualLoadRewardedVideo(bool isOn)
    {
        CFSetManualLoadRewardedVideo(isOn);
    }

    public void setNetworkData(string networkKey, string networkData)
    {
        CFSetNetworkData(networkKey, networkData);
    }

    [AOT.MonoPInvokeCallback(typeof(ISUnityPauseGame))]
    public void SetPauseGame(bool pause)
    {
        RegisterPauseGameFunction(pause);
        if (pause)
        {
            setMetaData("IS_PAUSE_GAME_FLAG", "true");
        }
        else
        {
            setMetaData("IS_PAUSE_GAME_FLAG", "false");
        }
    }

    //******************* SDK Init *******************//

    public void setUserId(string userId)
    {
        CFSetUserId(userId);
    }

    public void init(string appKey)
    {
        CFSetPluginData("Unity", IronSource.pluginVersion(), IronSource.unityVersion());
        LevelPlayLogger.LogVerbose("IntegrationHelper pluginVersion: " + IronSource.pluginVersion());
        CFInit(appKey);
    }

    public void init(string appKey, params string[] adUnits)
    {
        CFSetPluginData("Unity", IronSource.pluginVersion(), IronSource.unityVersion());
        LevelPlayLogger.LogVerbose("IntegrationHelper pluginVersion: " + IronSource.pluginVersion());
        CFInitWithAdUnits(appKey, adUnits);
    }

    //******************* RewardedVideo API *******************//

    public void loadRewardedVideo()
    {
        CFLoadRewardedVideo();
    }

    public void showRewardedVideo()
    {
        CFShowRewardedVideo();
    }

    public void showRewardedVideo(string placementName)
    {
        CFShowRewardedVideoWithPlacementName(placementName);
    }

    public bool isRewardedVideoAvailable()
    {
        return CFIsRewardedVideoAvailable();
    }

    public bool isRewardedVideoPlacementCapped(string placementName)
    {
        return CFIsRewardedVideoPlacementCapped(placementName);
    }

    public IronSourcePlacement getPlacementInfo(string placementName)
    {
        IronSourcePlacement sp = null;

        string spString = CFGetPlacementInfo(placementName);
        if (spString != null)
        {
            Dictionary<string, object> spDic = IronSourceJSON.Json.Deserialize(spString) as Dictionary<string, object>;
            string pName = spDic["placement_name"].ToString();
            string rewardName = spDic["reward_name"].ToString();
            int rewardAmount = Convert.ToInt32(spDic["reward_amount"].ToString());
            sp = new IronSourcePlacement(pName, rewardName, rewardAmount);
        }

        return sp;
    }

    public void setRewardedVideoServerParams(Dictionary<string, string> parameters)
    {
        string json = IronSourceJSON.Json.Serialize(parameters);
        CFSetRewardedVideoServerParameters(json);
    }

    public void clearRewardedVideoServerParams()
    {
        CFClearRewardedVideoServerParameters();
    }

    //******************* Interstitial API *******************//

    public void loadInterstitial()
    {
        CFLoadInterstitial();
    }

    public void showInterstitial()
    {
        CFShowInterstitial();
    }

    public void showInterstitial(string placementName)
    {
        CFShowInterstitialWithPlacementName(placementName);
    }

    public bool isInterstitialReady()
    {
        return CFIsInterstitialReady();
    }

    public bool isInterstitialPlacementCapped(string placementName)
    {
        return CFIsInterstitialPlacementCapped(placementName);
    }

    //******************* Banner API *******************//

    public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position)
    {
        loadBanner(size, position, "");
    }

    public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position, string placementName)
    {
        CFLoadBanner(size.Description, (int)size.Width, (int)size.Height, (int)position, placementName, (bool)size.IsAdaptiveEnabled(), (float)size.getBannerContainerParams().Width, (float)size.getBannerContainerParams().Height);
    }

    public void destroyBanner()
    {
        CFDestroyBanner();
    }

    public void displayBanner()
    {
        CFDisplayBanner();
    }

    public void hideBanner()
    {
        CFHideBanner();
    }

    public bool isBannerPlacementCapped(string placementName)
    {
        return CFIsBannerPlacementCapped(placementName);
    }

    /// <summary>
    ///  Get the adaptive height according to the width.
    /// </summary>
    /// <param name="width"> The device width </param>
    public float getMaximalAdaptiveHeight(float width)
    {
        return CFIGetMaximalAdaptiveHeight(width);
    }

    /// <summary>
    ///  Get device width in Point adjust to safe area
    /// </summary>
    public float getDeviceScreenWidth()
    {
        return CFIGetDeviceScreenWidth();
    }

    public void setSegment(IronSourceSegment segment)
    {
        Dictionary<string, string> dict = segment.getSegmentAsDict();
        string json = IronSourceJSON.Json.Serialize(dict);
        CFSetSegment(json);
    }

    public void setConsent(bool consent)
    {
        CFSetConsent(consent);
    }

    public void loadConsentViewWithType(string consentViewType)
    {
        CFLoadConsentViewWithType(consentViewType);
    }

    public void showConsentViewWithType(string consentViewType)
    {
        CFShowConsentViewWithType(consentViewType);
    }

    //******************* ILRD API *******************//

    public void setAdRevenueData(string dataSource, Dictionary<string, string> impressionData)
    {
        string json = IronSourceJSON.Json.Serialize(impressionData);
        CFSetAdRevenueData(dataSource, json);
    }

    //******************* TestSuite API *******************//

    public void launchTestSuite()
    {
        LevelPlayLogger.LogVerbose("iOSAgent: launching TestSuite");
        CFLaunchTestSuite();
    }

    #endregion
}
#endif
