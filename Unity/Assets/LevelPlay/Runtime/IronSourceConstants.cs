using System;

[Obsolete("This class will be made private in a future version.", false)]
public static class IronSourceConstants
{
    //General
    public const string GET_INSTANCE_KEY = "getInstance";
    public const string ERROR_CODE = "error_code";
    public const string ERROR_DESCRIPTION = "error_description";
    public const string EMPTY_STRING = "";

    //Bridge Classes
    public const string LevelPlayinterstitialBridgeListenerClass = "com.ironsource.unity.androidbridge.UnityLevelPlayInterstitialListener";
    public const string LevelPlayRewardedVideoBridgeListenerClass = "com.ironsource.unity.androidbridge.UnityLevelPlayRewardedVideoListener";
    public const string LevelPlayRewardedVideoManualBridgeListenerClass = "com.ironsource.unity.androidbridge.UnityLevelPlayRewardedVideoManualListener";
    public const string bridgeClass = "com.ironsource.unity.androidbridge.AndroidBridge";
    public const string LevelPlaybannerBridgeListenerClass = "com.ironsource.unity.androidbridge.UnityLevelPlayBannerListener";
    public const string segmentBridgeListenerClass = "com.ironsource.unity.androidbridge.UnitySegmentListener";
    public const string impressionDataBridgeListenerClass = "com.ironsource.unity.androidbridge.UnityImpressionDataListener";
    public const string initializeBridgeListenerClass = "com.ironsource.unity.androidbridge.UnityInitializationListener";

    //Impression Data
    public const string IMPRESSION_DATA_KEY_AUCTION_ID = "auctionId";
    public const string IMPRESSION_DATA_KEY_AD_UNIT = "adUnit";
    internal const string IMPRESSION_DATA_KEY_AD_FORMAT = "adFormat";
    internal const string IMPRESSION_DATA_KEY_MEDIATION_AD_UNIT_NAME = "mediationAdUnitName";
    internal const string IMPRESSION_DATA_KEY_MEDIATION_AD_UNIT_ID = "mediationAdUnitId";
    public const string IMPRESSION_DATA_KEY_COUNTRY = "country";
    public const string IMPRESSION_DATA_KEY_ABTEST = "ab";
    public const string IMPRESSION_DATA_KEY_SEGMENT_NAME = "segmentName";
    public const string IMPRESSION_DATA_KEY_PLACEMENT = "placement";
    public const string IMPRESSION_DATA_KEY_AD_NETWORK = "adNetwork";
    public const string IMPRESSION_DATA_KEY_INSTANCE_NAME = "instanceName";
    public const string IMPRESSION_DATA_KEY_INSTANCE_ID = "instanceId";
    public const string IMPRESSION_DATA_KEY_REVENUE = "revenue";
    public const string IMPRESSION_DATA_KEY_PRECISION = "precision";
    public const string IMPRESSION_DATA_KEY_LIFETIME_REVENUE = "lifetimeRevenue";
    public const string IMPRESSION_DATA_KEY_ENCRYPTED_CPM = "encryptedCPM";
    public const string IMPRESSION_DATA_KEY_CONVERSION_VALUE = "conversionValue";
    public const string GENDER_MALE = "male";
    public const string GENDER_FEMALE = "female";
    public const string GENDER_UNKNOWN = "unknown";

    //Mediation Settings
    public const string IRONSOURCE_MEDIATION_SETTING_NAME = "IronSourceMediationSettings";
    public const string IRONSOURCE_MEDIATED_NETWORK_SETTING_NAME = "IronSourceMediatedNetworkSettings";
    public const string IRONSOURCE_RESOURCES_PATH = "Assets/LevelPlay/Resources";
    public const string IRONSOURCE_SKAN_ID_KEY = "su67r6k2v3.skadnetwork";
}
