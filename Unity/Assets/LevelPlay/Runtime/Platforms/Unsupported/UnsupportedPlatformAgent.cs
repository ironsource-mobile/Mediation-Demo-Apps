using System;
using System.Collections.Generic;
using Unity.Services.LevelPlay;

[Obsolete("Please use IronSource.Agent", false)]
public class UnsupportedPlatformAgent : IronSourceIAgent
{
    public UnsupportedPlatformAgent()
    {
        LevelPlayLogger.Log("Generated with Unity editor");
    }

    #region IronSourceAgent implementation

    /// <summary>
    /// This function is not supported on the current platform and does nothing.
    /// </summary>
    /// <param name="waterfallConfiguration">The configuration for the given ad types waterfall. </param>
    /// <param name="adFormat">The AdFormat for which to configure the waterfall.</param>
    public void SetWaterfallConfiguration(WaterfallConfiguration waterfallConfiguration, AdFormat adFormat)
    {
    }

    public void start()
    {
    }

    //******************* Base API *******************//

    public void onApplicationPause(bool pause)
    {
    }

    public string getAdvertiserId()
    {
        return "";
    }

    public void validateIntegration()
    {
    }

    public void shouldTrackNetworkState(bool track)
    {
    }

    public bool setDynamicUserId(string dynamicUserId)
    {
        return false;
    }

    public void setAdaptersDebug(bool enabled)
    {
    }

    public void setMetaData(string key, string value)
    {
    }

    public void setMetaData(string key, params string[] values)
    {
    }

    public int? getConversionValue()
    {
        return null;
    }

    public void setManualLoadRewardedVideo(bool isOn)
    {
    }

    public void setNetworkData(string networkKey, string networkDataJson)
    {
    }

    public void SetPauseGame(bool pause)
    {
    }

    //******************* SDK Init *******************//

    public void setUserId(string userId)
    {
    }

    public void init(string appKey)
    {
    }

    public void init(string appKey, params string[] adUnits)
    {
    }

    //******************* RewardedVideo API *******************//

    public void loadRewardedVideo()
    {
    }

    public void showRewardedVideo()
    {
    }

    public void showRewardedVideo(string placementName)
    {
    }

    public bool isRewardedVideoAvailable()
    {
        return false;
    }

    public bool isRewardedVideoPlacementCapped(string placementName)
    {
        return true;
    }

    public IronSourcePlacement getPlacementInfo(string placementName)
    {
        return null;
    }

    public void setRewardedVideoServerParams(Dictionary<string, string> parameters)
    {
    }

    public void clearRewardedVideoServerParams()
    {
    }

    //******************* Interstitial API *******************//

    public void loadInterstitial()
    {
    }

    public void showInterstitial()
    {
    }

    public void showInterstitial(string placementName)
    {
    }

    public bool isInterstitialReady()
    {
        return false;
    }

    public bool isInterstitialPlacementCapped(string placementName)
    {
        return true;
    }

    //******************* Banner API *******************//

    public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position)
    {
    }

    public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position, string placementName)
    {
    }

    public void destroyBanner()
    {
    }

    public void displayBanner()
    {
    }

    public void hideBanner()
    {
    }

    public bool isBannerPlacementCapped(string placementName)
    {
        return false;
    }

    public float getMaximalAdaptiveHeight(float width)
    {
        return -1;
    }

    public float getDeviceScreenWidth()
    {
        return -1;
    }

    public void setBannerContainerParams(ISContainerParams parameters)
    {
    }

    public void setSegment(IronSourceSegment segment)
    {
    }

    public void setConsent(bool consent)
    {
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
    }

    //******************* TestSuite API *******************//

    public void launchTestSuite()
    {
        LevelPlayLogger.Log("Unsupported Platform");
    }

    #endregion
}
