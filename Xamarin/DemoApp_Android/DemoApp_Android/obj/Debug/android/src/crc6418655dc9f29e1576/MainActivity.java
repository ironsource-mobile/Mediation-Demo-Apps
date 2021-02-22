package crc6418655dc9f29e1576;


public class MainActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.sdk.RewardedVideoListener,
		com.ironsource.mediationsdk.sdk.OfferwallListener,
		com.ironsource.mediationsdk.sdk.InterstitialListener,
		com.ironsource.mediationsdk.sdk.BannerListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onRewardedVideoAdClicked:(Lcom/ironsource/mediationsdk/model/Placement;)V:GetOnRewardedVideoAdClicked_Lcom_ironsource_mediationsdk_model_Placement_Handler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdClosed:()V:GetOnRewardedVideoAdClosedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdEnded:()V:GetOnRewardedVideoAdEndedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdOpened:()V:GetOnRewardedVideoAdOpenedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdRewarded:(Lcom/ironsource/mediationsdk/model/Placement;)V:GetOnRewardedVideoAdRewarded_Lcom_ironsource_mediationsdk_model_Placement_Handler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnRewardedVideoAdShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdStarted:()V:GetOnRewardedVideoAdStartedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAvailabilityChanged:(Z)V:GetOnRewardedVideoAvailabilityChanged_ZHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onGetOfferwallCreditsFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnGetOfferwallCreditsFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallAdCredited:(IIZ)Z:GetOnOfferwallAdCredited_IIZHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallAvailable:(Z)V:GetOnOfferwallAvailable_ZHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallClosed:()V:GetOnOfferwallClosedHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallOpened:()V:GetOnOfferwallOpenedHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnOfferwallShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdClicked:()V:GetOnInterstitialAdClickedHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdClosed:()V:GetOnInterstitialAdClosedHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdLoadFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnInterstitialAdLoadFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdOpened:()V:GetOnInterstitialAdOpenedHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdReady:()V:GetOnInterstitialAdReadyHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnInterstitialAdShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdShowSucceeded:()V:GetOnInterstitialAdShowSucceededHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdClicked:()V:GetOnBannerAdClickedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdLeftApplication:()V:GetOnBannerAdLeftApplicationHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdLoadFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnBannerAdLoadFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdLoaded:()V:GetOnBannerAdLoadedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdScreenDismissed:()V:GetOnBannerAdScreenDismissedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdScreenPresented:()V:GetOnBannerAdScreenPresentedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("DemoApp_Android.MainActivity, Xamarin Integration App", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("DemoApp_Android.MainActivity, Xamarin Integration App", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onRewardedVideoAdClicked (com.ironsource.mediationsdk.model.Placement p0)
	{
		n_onRewardedVideoAdClicked (p0);
	}

	private native void n_onRewardedVideoAdClicked (com.ironsource.mediationsdk.model.Placement p0);


	public void onRewardedVideoAdClosed ()
	{
		n_onRewardedVideoAdClosed ();
	}

	private native void n_onRewardedVideoAdClosed ();


	public void onRewardedVideoAdEnded ()
	{
		n_onRewardedVideoAdEnded ();
	}

	private native void n_onRewardedVideoAdEnded ();


	public void onRewardedVideoAdOpened ()
	{
		n_onRewardedVideoAdOpened ();
	}

	private native void n_onRewardedVideoAdOpened ();


	public void onRewardedVideoAdRewarded (com.ironsource.mediationsdk.model.Placement p0)
	{
		n_onRewardedVideoAdRewarded (p0);
	}

	private native void n_onRewardedVideoAdRewarded (com.ironsource.mediationsdk.model.Placement p0);


	public void onRewardedVideoAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0)
	{
		n_onRewardedVideoAdShowFailed (p0);
	}

	private native void n_onRewardedVideoAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0);


	public void onRewardedVideoAdStarted ()
	{
		n_onRewardedVideoAdStarted ();
	}

	private native void n_onRewardedVideoAdStarted ();


	public void onRewardedVideoAvailabilityChanged (boolean p0)
	{
		n_onRewardedVideoAvailabilityChanged (p0);
	}

	private native void n_onRewardedVideoAvailabilityChanged (boolean p0);


	public void onGetOfferwallCreditsFailed (com.ironsource.mediationsdk.logger.IronSourceError p0)
	{
		n_onGetOfferwallCreditsFailed (p0);
	}

	private native void n_onGetOfferwallCreditsFailed (com.ironsource.mediationsdk.logger.IronSourceError p0);


	public boolean onOfferwallAdCredited (int p0, int p1, boolean p2)
	{
		return n_onOfferwallAdCredited (p0, p1, p2);
	}

	private native boolean n_onOfferwallAdCredited (int p0, int p1, boolean p2);


	public void onOfferwallAvailable (boolean p0)
	{
		n_onOfferwallAvailable (p0);
	}

	private native void n_onOfferwallAvailable (boolean p0);


	public void onOfferwallClosed ()
	{
		n_onOfferwallClosed ();
	}

	private native void n_onOfferwallClosed ();


	public void onOfferwallOpened ()
	{
		n_onOfferwallOpened ();
	}

	private native void n_onOfferwallOpened ();


	public void onOfferwallShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0)
	{
		n_onOfferwallShowFailed (p0);
	}

	private native void n_onOfferwallShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0);


	public void onInterstitialAdClicked ()
	{
		n_onInterstitialAdClicked ();
	}

	private native void n_onInterstitialAdClicked ();


	public void onInterstitialAdClosed ()
	{
		n_onInterstitialAdClosed ();
	}

	private native void n_onInterstitialAdClosed ();


	public void onInterstitialAdLoadFailed (com.ironsource.mediationsdk.logger.IronSourceError p0)
	{
		n_onInterstitialAdLoadFailed (p0);
	}

	private native void n_onInterstitialAdLoadFailed (com.ironsource.mediationsdk.logger.IronSourceError p0);


	public void onInterstitialAdOpened ()
	{
		n_onInterstitialAdOpened ();
	}

	private native void n_onInterstitialAdOpened ();


	public void onInterstitialAdReady ()
	{
		n_onInterstitialAdReady ();
	}

	private native void n_onInterstitialAdReady ();


	public void onInterstitialAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0)
	{
		n_onInterstitialAdShowFailed (p0);
	}

	private native void n_onInterstitialAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0);


	public void onInterstitialAdShowSucceeded ()
	{
		n_onInterstitialAdShowSucceeded ();
	}

	private native void n_onInterstitialAdShowSucceeded ();


	public void onBannerAdClicked ()
	{
		n_onBannerAdClicked ();
	}

	private native void n_onBannerAdClicked ();


	public void onBannerAdLeftApplication ()
	{
		n_onBannerAdLeftApplication ();
	}

	private native void n_onBannerAdLeftApplication ();


	public void onBannerAdLoadFailed (com.ironsource.mediationsdk.logger.IronSourceError p0)
	{
		n_onBannerAdLoadFailed (p0);
	}

	private native void n_onBannerAdLoadFailed (com.ironsource.mediationsdk.logger.IronSourceError p0);


	public void onBannerAdLoaded ()
	{
		n_onBannerAdLoaded ();
	}

	private native void n_onBannerAdLoaded ();


	public void onBannerAdScreenDismissed ()
	{
		n_onBannerAdScreenDismissed ();
	}

	private native void n_onBannerAdScreenDismissed ();


	public void onBannerAdScreenPresented ()
	{
		n_onBannerAdScreenPresented ();
	}

	private native void n_onBannerAdScreenPresented ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
