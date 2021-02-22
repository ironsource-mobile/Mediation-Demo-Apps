package mono.com.ironsource.mediationsdk.utils;


public class ContextProvider_ContextLifeCycleListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.ironsource.mediationsdk.utils.ContextProvider.ContextLifeCycleListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPause:(Landroid/app/Activity;)V:GetOnPause_Landroid_app_Activity_Handler:Com.Ironsource.Mediationsdk.Utils.ContextProvider/IContextLifeCycleListenerInvoker, IronSource-Android_v6.17.0\n" +
			"n_onResume:(Landroid/app/Activity;)V:GetOnResume_Landroid_app_Activity_Handler:Com.Ironsource.Mediationsdk.Utils.ContextProvider/IContextLifeCycleListenerInvoker, IronSource-Android_v6.17.0\n" +
			"";
		mono.android.Runtime.register ("Com.Ironsource.Mediationsdk.Utils.ContextProvider+IContextLifeCycleListenerImplementor, IronSource-Android_v6.17.0", ContextProvider_ContextLifeCycleListenerImplementor.class, __md_methods);
	}


	public ContextProvider_ContextLifeCycleListenerImplementor ()
	{
		super ();
		if (getClass () == ContextProvider_ContextLifeCycleListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Ironsource.Mediationsdk.Utils.ContextProvider+IContextLifeCycleListenerImplementor, IronSource-Android_v6.17.0", "", this, new java.lang.Object[] {  });
	}


	public void onPause (android.app.Activity p0)
	{
		n_onPause (p0);
	}

	private native void n_onPause (android.app.Activity p0);


	public void onResume (android.app.Activity p0)
	{
		n_onResume (p0);
	}

	private native void n_onResume (android.app.Activity p0);

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
