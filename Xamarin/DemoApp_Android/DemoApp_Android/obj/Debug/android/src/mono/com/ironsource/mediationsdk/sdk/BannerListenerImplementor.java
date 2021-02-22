package mono.com.ironsource.mediationsdk.sdk;


public class BannerListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.sdk.BannerListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBannerAdClicked:()V:GetOnBannerAdClickedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdLeftApplication:()V:GetOnBannerAdLeftApplicationHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdLoadFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnBannerAdLoadFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdLoaded:()V:GetOnBannerAdLoadedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdScreenDismissed:()V:GetOnBannerAdScreenDismissedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onBannerAdScreenPresented:()V:GetOnBannerAdScreenPresentedHandler:Com.Ironsource.Mediationsdk.Sdk.IBannerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.Sdk.IBannerListenerImplementor, IronSource-Android_v6.17.0", BannerListenerImplementor.class, __md_methods);
	}


	public BannerListenerImplementor ()
	{
		super ();
		if (getClass () == BannerListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.Sdk.IBannerListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


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
