package mono.com.ironsource.mediationsdk.sdk;


public class RewardedVideoListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.sdk.RewardedVideoListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRewardedVideoAdClicked:(Lcom/ironsource/mediationsdk/model/Placement;)V:GetOnRewardedVideoAdClicked_Lcom_ironsource_mediationsdk_model_Placement_Handler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdClosed:()V:GetOnRewardedVideoAdClosedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdEnded:()V:GetOnRewardedVideoAdEndedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdOpened:()V:GetOnRewardedVideoAdOpenedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdRewarded:(Lcom/ironsource/mediationsdk/model/Placement;)V:GetOnRewardedVideoAdRewarded_Lcom_ironsource_mediationsdk_model_Placement_Handler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnRewardedVideoAdShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdStarted:()V:GetOnRewardedVideoAdStartedHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAvailabilityChanged:(Z)V:GetOnRewardedVideoAvailabilityChanged_ZHandler:Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerImplementor, IronSource-Android_v6.17.0", RewardedVideoListenerImplementor.class, __md_methods);
	}


	public RewardedVideoListenerImplementor ()
	{
		super ();
		if (getClass () == RewardedVideoListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.Sdk.IRewardedVideoListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


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
