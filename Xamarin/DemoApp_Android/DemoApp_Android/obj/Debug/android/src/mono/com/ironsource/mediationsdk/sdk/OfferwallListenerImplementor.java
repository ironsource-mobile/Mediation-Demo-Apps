package mono.com.ironsource.mediationsdk.sdk;


public class OfferwallListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.sdk.OfferwallListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onGetOfferwallCreditsFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnGetOfferwallCreditsFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallAdCredited:(IIZ)Z:GetOnOfferwallAdCredited_IIZHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallAvailable:(Z)V:GetOnOfferwallAvailable_ZHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallClosed:()V:GetOnOfferwallClosedHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallOpened:()V:GetOnOfferwallOpenedHandler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onOfferwallShowFailed:(Lcom/ironsource/mediationsdk/logger/IronSourceError;)V:GetOnOfferwallShowFailed_Lcom_ironsource_mediationsdk_logger_IronSourceError_Handler:Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerImplementor, IronSource-Android_v6.17.0", OfferwallListenerImplementor.class, __md_methods);
	}


	public OfferwallListenerImplementor ()
	{
		super ();
		if (getClass () == OfferwallListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.Sdk.IOfferwallListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


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
