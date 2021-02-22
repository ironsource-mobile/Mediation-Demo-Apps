package mono.com.ironsource.mediationsdk.sdk;


public class InterstitialListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.sdk.InterstitialListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterstitialAdClicked:()V:GetOnInterstitialAdClickedHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdClosed:()V:GetOnInterstitialAdClosedHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdLoadFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnInterstitialAdLoadFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdOpened:()V:GetOnInterstitialAdOpenedHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdReady:()V:GetOnInterstitialAdReadyHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnInterstitialAdShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdShowSucceeded:()V:GetOnInterstitialAdShowSucceededHandler:Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerImplementor, IronSource-Android_v6.17.0", InterstitialListenerImplementor.class, __md_methods);
	}


	public InterstitialListenerImplementor ()
	{
		super ();
		if (getClass () == InterstitialListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.Sdk.IInterstitialListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


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
