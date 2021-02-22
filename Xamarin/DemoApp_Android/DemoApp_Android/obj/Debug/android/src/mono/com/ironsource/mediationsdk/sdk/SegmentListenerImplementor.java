package mono.com.ironsource.mediationsdk.sdk;


public class SegmentListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.sdk.SegmentListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSegmentReceived:(Ljava/lang/String;)V:GetOnSegmentReceived_Ljava_lang_String_Handler:Com.Ironsource.Mediationsdk.Sdk.ISegmentListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.Sdk.ISegmentListenerImplementor, IronSource-Android_v6.17.0", SegmentListenerImplementor.class, __md_methods);
	}


	public SegmentListenerImplementor ()
	{
		super ();
		if (getClass () == SegmentListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.Sdk.ISegmentListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


	public void onSegmentReceived (java.lang.String p0)
	{
		n_onSegmentReceived (p0);
	}

	private native void n_onSegmentReceived (java.lang.String p0);

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
