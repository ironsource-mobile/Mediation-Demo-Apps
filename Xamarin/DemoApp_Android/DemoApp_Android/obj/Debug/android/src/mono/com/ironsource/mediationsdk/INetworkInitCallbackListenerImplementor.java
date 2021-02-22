package mono.com.ironsource.mediationsdk;


public class INetworkInitCallbackListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.INetworkInitCallbackListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNetworkInitCallbackFailed:(Ljava/lang/String;)V:GetOnNetworkInitCallbackFailed_Ljava_lang_String_Handler:Com.Ironsource.Mediationsdk.INetworkInitCallbackListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onNetworkInitCallbackLoadSuccess:(Ljava/lang/String;)V:GetOnNetworkInitCallbackLoadSuccess_Ljava_lang_String_Handler:Com.Ironsource.Mediationsdk.INetworkInitCallbackListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onNetworkInitCallbackSuccess:()V:GetOnNetworkInitCallbackSuccessHandler:Com.Ironsource.Mediationsdk.INetworkInitCallbackListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.INetworkInitCallbackListenerImplementor, IronSource-Android_v6.17.0", INetworkInitCallbackListenerImplementor.class, __md_methods);
	}


	public INetworkInitCallbackListenerImplementor ()
	{
		super ();
		if (getClass () == INetworkInitCallbackListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.INetworkInitCallbackListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


	public void onNetworkInitCallbackFailed (java.lang.String p0)
	{
		n_onNetworkInitCallbackFailed (p0);
	}

	private native void n_onNetworkInitCallbackFailed (java.lang.String p0);


	public void onNetworkInitCallbackLoadSuccess (java.lang.String p0)
	{
		n_onNetworkInitCallbackLoadSuccess (p0);
	}

	private native void n_onNetworkInitCallbackLoadSuccess (java.lang.String p0);


	public void onNetworkInitCallbackSuccess ()
	{
		n_onNetworkInitCallbackSuccess ();
	}

	private native void n_onNetworkInitCallbackSuccess ();

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
