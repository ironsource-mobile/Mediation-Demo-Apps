using System;
using UnityEngine;
using System.Collections.Generic;
using com.unity3d.mediation;

// Example for IronSource Unity.
public class LevelPlaySample : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    private LevelPlayInterstitialAd interstitialAd;
    
#if UNITY_ANDROID
    string appKey = "85460dcd";
    string bannerAdUnitId = "thnfvcsog13bhn08";
    string interstitialAdUnitId = "aeyqi3vqlv6o8sh9";
#elif UNITY_IPHONE
    string appKey = "8545d445";
    string bannerAdUnitId = "iep3rxsyp9na3rw8";
    string interstitialAdUnitId = "wmgt0712uuux8ju4";
#else
    string appKey = "unexpected_platform";
    string bannerAdUnitId = "unexpected_platform";
    string interstitialAdUnitId = "unexpected_platform";
#endif



    public void Start()
    {
        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        Debug.Log("unity-script: LevelPlay SDK initialization");
        LevelPlay.Init(appKey,adFormats:new []{LevelPlayAdFormat.REWARDED});
        
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
    }

    void EnableAds()
    {
        //Add ImpressionSuccess Event
        IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        
        bannerAd = new LevelPlayBannerAd(bannerAdUnitId);

        // Register to Banner events
        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;

        // Create Interstitial object
        interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);
        
        // Register to Interstitial events
        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;
    }

    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void OnGUI()
    {
        GUI.backgroundColor = Color.blue;
        GUI.skin.button.fontSize = (int)(0.035f * Screen.width);


        Rect showRewardedVideoButton = new Rect(0.10f * Screen.width, 0.15f * Screen.height, 0.80f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(showRewardedVideoButton, "Show Rewarded Video"))
        {
            Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
            }
        }

        Rect loadInterstitialButton = new Rect(0.10f * Screen.width, 0.25f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(loadInterstitialButton, "Load Interstitial"))
        {
            Debug.Log("unity-script: LoadInterstitialButtonClicked");
            interstitialAd.LoadAd();
        }

        Rect showInterstitialButton = new Rect(0.55f * Screen.width, 0.25f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(showInterstitialButton, "Show Interstitial"))
        {
            Debug.Log("unity-script: ShowInterstitialButtonClicked");
            if (interstitialAd.IsAdReady())
            {
                interstitialAd.ShowAd();
            }
            else
            {
                Debug.Log("unity-script: Levelplay Interstital Ad Ready? - False");
            }
        }

        Rect loadBannerButton = new Rect(0.10f * Screen.width, 0.35f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(loadBannerButton, "Load Banner"))
        {
            Debug.Log("unity-script: loadBannerButtonClicked");
            bannerAd.LoadAd();
        }

        Rect hideBannerButton = new Rect(0.55f * Screen.width, 0.35f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
        if (GUI.Button(hideBannerButton, "Hide Banner"))
        {
            Debug.Log("unity-script: HideButtonClicked");
            bannerAd.HideAd();
        }
    }

    #region Init callback handlers

    void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent with config: "+ config);
        EnableAds();
    }
    
    void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.Log("unity-script: I got SdkInitializationFailedEvent with error: "+ error);
    }

    #endregion

    #region AdInfo Rewarded Video
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdShowFailedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }

    #endregion
    #region AdInfo Interstitial

    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadFailedEvent With Error " + error);
    }
	
    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdDisplayedEvent With AdInfo " + adInfo);
    }
	
    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError infoError)
    {
        Debug.Log("unity-script: I got InterstitialOnAdDisplayFailedEvent With InfoError " + infoError);
    }
	
    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo);
    }
	
    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdInfoChangedEvent With AdInfo " + adInfo);
    }

    #endregion

    #region Banner AdInfo

    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdLoadedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError)
    {
        Debug.Log("unity-script: I got BannerOnAdLoadFailedEvent With Error " + ironSourceError);
    }

    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdClickedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdDisplayedEvent With AdInfo " + adInfo);
    }
	
    void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError)
    {
        Debug.Log("unity-script: I got BannerOnAdDisplayFailedEvent With AdInfoError " + adInfoError);
    }

    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdCollapsedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdLeftApplicationEvent With AdInfo " + adInfo);
    }

    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdExpandedEvent With AdInfo " + adInfo);
    }

    #endregion

    #region ImpressionSuccess callback handler

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
    }

    #endregion

    private void OnDisable()
    {
        bannerAd?.DestroyAd();
        interstitialAd?.DestroyAd();
    }
}
