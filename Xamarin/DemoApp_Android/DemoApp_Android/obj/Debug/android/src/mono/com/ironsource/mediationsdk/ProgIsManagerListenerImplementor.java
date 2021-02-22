package mono.com.ironsource.mediationsdk;


public class ProgIsManagerListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.ProgIsManagerListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterstitialAdClicked:(Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialAdClicked_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdClosed:(Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialAdClosed_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdLoadFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;Lcom/ironsource/mediationsdk/ProgIsSmash;J)V:GetOnInterstitialAdLoadFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Lcom_ironsource_mediationsdk_ProgIsSmash_JHandler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdOpened:(Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialAdOpened_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdReady:(Lcom/ironsource/mediationsdk/ProgIsSmash;J)V:GetOnInterstitialAdReady_Lcom_ironsource_mediationsdk_ProgIsSmash_JHandler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialAdShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdShowSucceeded:(Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialAdShowSucceeded_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialAdVisible:(Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialAdVisible_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialInitFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialInitFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onInterstitialInitSuccess:(Lcom/ironsource/mediationsdk/ProgIsSmash;)V:GetOnInterstitialInitSuccess_Lcom_ironsource_mediationsdk_ProgIsSmash_Handler:Com.Ironsource.Mediationsdk.IProgIsManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.IProgIsManagerListenerImplementor, IronSource-Android_v6.17.0", ProgIsManagerListenerImplementor.class, __md_methods);
	}


	public ProgIsManagerListenerImplementor ()
	{
		super ();
		if (getClass () == ProgIsManagerListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.IProgIsManagerListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


	public void onInterstitialAdClicked (com.ironsource.mediationsdk.ProgIsSmash p0)
	{
		n_onInterstitialAdClicked (p0);
	}

	private native void n_onInterstitialAdClicked (com.ironsource.mediationsdk.ProgIsSmash p0);


	public void onInterstitialAdClosed (com.ironsource.mediationsdk.ProgIsSmash p0)
	{
		n_onInterstitialAdClosed (p0);
	}

	private native void n_onInterstitialAdClosed (com.ironsource.mediationsdk.ProgIsSmash p0);


	public void onInterstitialAdLoadFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgIsSmash p1, long p2)
	{
		n_onInterstitialAdLoadFailed (p0, p1, p2);
	}

	private native void n_onInterstitialAdLoadFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgIsSmash p1, long p2);


	public void onInterstitialAdOpened (com.ironsource.mediationsdk.ProgIsSmash p0)
	{
		n_onInterstitialAdOpened (p0);
	}

	private native void n_onInterstitialAdOpened (com.ironsource.mediationsdk.ProgIsSmash p0);


	public void onInterstitialAdReady (com.ironsource.mediationsdk.ProgIsSmash p0, long p1)
	{
		n_onInterstitialAdReady (p0, p1);
	}

	private native void n_onInterstitialAdReady (com.ironsource.mediationsdk.ProgIsSmash p0, long p1);


	public void onInterstitialAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgIsSmash p1)
	{
		n_onInterstitialAdShowFailed (p0, p1);
	}

	private native void n_onInterstitialAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgIsSmash p1);


	public void onInterstitialAdShowSucceeded (com.ironsource.mediationsdk.ProgIsSmash p0)
	{
		n_onInterstitialAdShowSucceeded (p0);
	}

	private native void n_onInterstitialAdShowSucceeded (com.ironsource.mediationsdk.ProgIsSmash p0);


	public void onInterstitialAdVisible (com.ironsource.mediationsdk.ProgIsSmash p0)
	{
		n_onInterstitialAdVisible (p0);
	}

	private native void n_onInterstitialAdVisible (com.ironsource.mediationsdk.ProgIsSmash p0);


	public void onInterstitialInitFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgIsSmash p1)
	{
		n_onInterstitialInitFailed (p0, p1);
	}

	private native void n_onInterstitialInitFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgIsSmash p1);


	public void onInterstitialInitSuccess (com.ironsource.mediationsdk.ProgIsSmash p0)
	{
		n_onInterstitialInitSuccess (p0);
	}

	private native void n_onInterstitialInitSuccess (com.ironsource.mediationsdk.ProgIsSmash p0);

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
