using UnityEngine;
using System;
using System.Collections;
using com.unity3d.mediation;

public class ShowInterstitialScript : MonoBehaviour
{
	GameObject InitText;
	GameObject LoadButton;
	GameObject LoadText;
	GameObject ShowButton;
	GameObject ShowText;

#if UNITY_ANDROID
    string interstitialAdUnitId = "5oairr7p0zffyhw7";
#elif UNITY_IPHONE
	string interstitialAdUnitId = "aqfm843a19c95imq";
//#if UNITY_ANDROID
//	string interstitialAdUnitId = "aeyqi3vqlv6o8sh9";
//#elif UNITY_IPHONE
//	string interstitialAdUnitId = "wmgt0712uuux8ju4";
#else
	string interstitialAdUnitId = "unexpected_platform";
#endif

    public static String INTERSTITIAL_INSTANCE_ID = "0";

    private LevelPlayInterstitialAd interstitialAd;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("unity-script: ShowInterstitialScript Start called");

		LoadButton = GameObject.Find ("LoadInterstitial");
		LoadText = GameObject.Find ("LoadInterstitialText");
		LoadText.GetComponent<UnityEngine.UI.Text> ().color = UnityEngine.Color.blue;
		
		ShowButton = GameObject.Find ("ShowInterstitial");
		ShowText = GameObject.Find ("ShowInterstitialText");
		ShowText.GetComponent<UnityEngine.UI.Text> ().color = UnityEngine.Color.red;
	}

	/************* Interstitial API *************/ 
	public void LoadInterstitialButtonClicked ()
	{
		// Create interstitial Ad
		interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);
		
		// Register to events
		interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
		interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
		interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
		interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
		interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
		interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
		interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;
		
		Debug.Log ("unity-script: LoadInterstitialButtonClicked");
		interstitialAd.LoadAd();
	}
	
	public void ShowInterstitialButtonClicked ()
	{
		Debug.Log ("unity-script: ShowInterstitialButtonClicked");
		if (interstitialAd.IsAdReady()) {
			interstitialAd.ShowAd();
		} else {
			Debug.Log ("unity-script: interstitialAd.IsAdReady - False");
		}
	}
	
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
	
	private void OnDestroy()
	{
		interstitialAd.DestroyAd();
	}
}