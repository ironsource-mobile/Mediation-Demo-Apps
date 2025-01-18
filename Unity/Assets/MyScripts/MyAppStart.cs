using System;
using UnityEngine;
using System.Collections;
using com.unity3d.mediation;
using Unity.VisualScripting;

public class MyAppStart : MonoBehaviour
{
	public static string uniqueUserId = "demoUserUnity";
	private LevelPlayBannerAd bannerAd;
	
#if UNITY_ANDROID
	const string appKey = "19a0d2e8d";
    const string bannerAdUnitId = "2sl8h7evrpm7vohp";
#elif UNITY_IPHONE
    const string appKey = "19a0da19d";
    const string bannerAdUnitId = "4b9fldhmt9jgeb5j";
//#if UNITY_ANDROID
//	string appKey = "85460dcd";
//	string bannerAdUnitId = "thnfvcsog13bhn08";
//#elif UNITY_IPHONE
//        string appKey = "8545d445";
//		string bannerAdUnitId = "iep3rxsyp9na3rw8";
#else
        string appKey = "unexpected_platform";
		string bannerAdUnitId = "unexpected_platform";
        string interstitialAdUnitId = "unexpected_platform";
#endif
	
	private void Awake()
	{
		Debug.Log ("unity-script: Awake called");

		//Dynamic config example
		IronSourceConfig.Instance.setClientSideCallbacks (true);

		string id = IronSource.Agent.getAdvertiserId ();
		Debug.Log ("unity-script: IronSource.Agent.getAdvertiserId : " + id);
		
		Debug.Log ("unity-script: IronSource.Agent.validateIntegration");
		IronSource.Agent.validateIntegration ();

		Debug.Log ("unity-script: unity version" + IronSource.unityVersion ());

		// SDK init
		Debug.Log ("unity-script: LevelPlay Init");
        LevelPlay.Init(appKey, adFormats: new[] { LevelPlayAdFormat.REWARDED });
        //LevelPlay.Init(appKey,uniqueUserId,new []{LevelPlayAdFormat.REWARDED});
		
		LevelPlay.OnInitSuccess += OnInitializationCompleted;
		LevelPlay.OnInitFailed += (error => Debug.Log("Initialization error: " + error));
	}
	
	void LoadBanner ()
	{
		// Create object
		bannerAd = new LevelPlayBannerAd(bannerAdUnitId);
		
		bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
		bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
		bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
		bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
		bannerAd.OnAdClicked += BannerOnAdClickedEvent;
		bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
		bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
		bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
		
		// Ad load
		bannerAd.LoadAd();
	}

	void OnInitializationCompleted(LevelPlayConfiguration configuration)
	{
		Debug.Log("Initialization completed");
		LoadBanner();

	}

	//Banner Events
	void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo)
	{
		Debug.Log("unity-script: I got BannerOnAdLoadedEvent With AdInfo " + adInfo);
	}

	void BannerOnAdLoadFailedEvent(LevelPlayAdError error)
	{
		Debug.Log("unity-script: I got BannerOnAdLoadFailedEvent With Error " + error);
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

	private void OnDestroy()
	{
		bannerAd.DestroyAd();
	}
}
