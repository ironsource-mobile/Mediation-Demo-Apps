using UnityEngine;
using System.Collections;

public class MyAppStart : MonoBehaviour
{
	public static string uniqueUserId = "demoUserUnity";


	// Use this for initialization
	void Start ()
	{
		#if UNITY_ANDROID
        string appKey = "85460dcd";
		#elif UNITY_IPHONE
        string appKey = "8545d445";
		#else
        string appKey = "unexpected_platform";
		#endif
		Debug.Log ("unity-script: MyAppStart Start called");

		//Dynamic config example
		IronSourceConfig.Instance.setClientSideCallbacks (true);

		string id = IronSource.Agent.getAdvertiserId ();
		Debug.Log ("unity-script: IronSource.Agent.getAdvertiserId : " + id);
		
		Debug.Log ("unity-script: IronSource.Agent.validateIntegration");
		IronSource.Agent.validateIntegration ();

		Debug.Log ("unity-script: unity version" + IronSource.unityVersion ());

		// Add Banner Events
		IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
		IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
		IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
		IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
		IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
		IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

		// SDK init
		Debug.Log ("unity-script: IronSource.Agent.init");
		IronSource.Agent.init (appKey);
		//IronSource.Agent.init (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        //IronSource.Agent.initISDemandOnly (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);

        //Set User ID For Server To Server Integration
        //// IronSource.Agent.setUserId ("UserId");
		
		// Load Banner example
		IronSource.Agent.loadBanner (IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnApplicationPause (bool isPaused)
	{
		Debug.Log ("unity-script: OnApplicationPause = " + isPaused);
		IronSource.Agent.onApplicationPause (isPaused);
	}

	//Banner Events
	void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
	{
		Debug.Log("unity-script: I got BannerOnAdLoadedEvent With AdInfo " + adInfo);
	}

	void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
	{
		Debug.Log("unity-script: I got BannerOnAdLoadFailedEvent With Error " + ironSourceError);
	}

	void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
	{
		Debug.Log("unity-script: I got BannerOnAdClickedEvent With AdInfo " + adInfo);
	}

	void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
	{
		Debug.Log("unity-script: I got BannerOnAdScreenPresentedEvent With AdInfo " + adInfo);
	}

	void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
	{
		Debug.Log("unity-script: I got BannerOnAdScreenDismissedEvent With AdInfo " + adInfo);
	}

	void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
	{
		Debug.Log("unity-script: I got BannerOnAdLeftApplicationEvent With AdInfo " + adInfo);
	}
}
