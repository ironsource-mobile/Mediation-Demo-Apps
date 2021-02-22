package mono.com.ironsource.mediationsdk;


public class ProgRvManagerListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.ProgRvManagerListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLoadError:(Lcom/ironsource/mediationsdk/ProgRvSmash;Ljava/lang/String;)V:GetOnLoadError_Lcom_ironsource_mediationsdk_ProgRvSmash_Ljava_lang_String_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onLoadSuccess:(Lcom/ironsource/mediationsdk/ProgRvSmash;Ljava/lang/String;)V:GetOnLoadSuccess_Lcom_ironsource_mediationsdk_ProgRvSmash_Ljava_lang_String_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdClicked:(Lcom/ironsource/mediationsdk/ProgRvSmash;Lcom/ironsource/mediationsdk/model/Placement;)V:GetOnRewardedVideoAdClicked_Lcom_ironsource_mediationsdk_ProgRvSmash_Lcom_ironsource_mediationsdk_model_Placement_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdClosed:(Lcom/ironsource/mediationsdk/ProgRvSmash;)V:GetOnRewardedVideoAdClosed_Lcom_ironsource_mediationsdk_ProgRvSmash_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdEnded:(Lcom/ironsource/mediationsdk/ProgRvSmash;)V:GetOnRewardedVideoAdEnded_Lcom_ironsource_mediationsdk_ProgRvSmash_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdOpened:(Lcom/ironsource/mediationsdk/ProgRvSmash;)V:GetOnRewardedVideoAdOpened_Lcom_ironsource_mediationsdk_ProgRvSmash_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdRewarded:(Lcom/ironsource/mediationsdk/ProgRvSmash;Lcom/ironsource/mediationsdk/model/Placement;)V:GetOnRewardedVideoAdRewarded_Lcom_ironsource_mediationsdk_ProgRvSmash_Lcom_ironsource_mediationsdk_model_Placement_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;Lcom/ironsource/mediationsdk/ProgRvSmash;)V:GetOnRewardedVideoAdShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Lcom_ironsource_mediationsdk_ProgRvSmash_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onRewardedVideoAdStarted:(Lcom/ironsource/mediationsdk/ProgRvSmash;)V:GetOnRewardedVideoAdStarted_Lcom_ironsource_mediationsdk_ProgRvSmash_Handler:Com.Ironsource.Mediationsdk.IProgRvManagerListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.IProgRvManagerListenerImplementor, IronSource-Android_v6.17.0", ProgRvManagerListenerImplementor.class, __md_methods);
	}


	public ProgRvManagerListenerImplementor ()
	{
		super ();
		if (getClass () == ProgRvManagerListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.IProgRvManagerListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


	public void onLoadError (com.ironsource.mediationsdk.ProgRvSmash p0, java.lang.String p1)
	{
		n_onLoadError (p0, p1);
	}

	private native void n_onLoadError (com.ironsource.mediationsdk.ProgRvSmash p0, java.lang.String p1);


	public void onLoadSuccess (com.ironsource.mediationsdk.ProgRvSmash p0, java.lang.String p1)
	{
		n_onLoadSuccess (p0, p1);
	}

	private native void n_onLoadSuccess (com.ironsource.mediationsdk.ProgRvSmash p0, java.lang.String p1);


	public void onRewardedVideoAdClicked (com.ironsource.mediationsdk.ProgRvSmash p0, com.ironsource.mediationsdk.model.Placement p1)
	{
		n_onRewardedVideoAdClicked (p0, p1);
	}

	private native void n_onRewardedVideoAdClicked (com.ironsource.mediationsdk.ProgRvSmash p0, com.ironsource.mediationsdk.model.Placement p1);


	public void onRewardedVideoAdClosed (com.ironsource.mediationsdk.ProgRvSmash p0)
	{
		n_onRewardedVideoAdClosed (p0);
	}

	private native void n_onRewardedVideoAdClosed (com.ironsource.mediationsdk.ProgRvSmash p0);


	public void onRewardedVideoAdEnded (com.ironsource.mediationsdk.ProgRvSmash p0)
	{
		n_onRewardedVideoAdEnded (p0);
	}

	private native void n_onRewardedVideoAdEnded (com.ironsource.mediationsdk.ProgRvSmash p0);


	public void onRewardedVideoAdOpened (com.ironsource.mediationsdk.ProgRvSmash p0)
	{
		n_onRewardedVideoAdOpened (p0);
	}

	private native void n_onRewardedVideoAdOpened (com.ironsource.mediationsdk.ProgRvSmash p0);


	public void onRewardedVideoAdRewarded (com.ironsource.mediationsdk.ProgRvSmash p0, com.ironsource.mediationsdk.model.Placement p1)
	{
		n_onRewardedVideoAdRewarded (p0, p1);
	}

	private native void n_onRewardedVideoAdRewarded (com.ironsource.mediationsdk.ProgRvSmash p0, com.ironsource.mediationsdk.model.Placement p1);


	public void onRewardedVideoAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgRvSmash p1)
	{
		n_onRewardedVideoAdShowFailed (p0, p1);
	}

	private native void n_onRewardedVideoAdShowFailed (com.ironsource.mediationsdk.logger.IronSourceError p0, com.ironsource.mediationsdk.ProgRvSmash p1);


	public void onRewardedVideoAdStarted (com.ironsource.mediationsdk.ProgRvSmash p0)
	{
		n_onRewardedVideoAdStarted (p0);
	}

	private native void n_onRewardedVideoAdStarted (com.ironsource.mediationsdk.ProgRvSmash p0);

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
