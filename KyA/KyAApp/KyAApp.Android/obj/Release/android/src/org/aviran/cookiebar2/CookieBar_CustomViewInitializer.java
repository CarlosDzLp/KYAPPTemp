package org.aviran.cookiebar2;


public class CookieBar_CustomViewInitializer
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		org.aviran.cookiebar2.CookieBar.CustomViewInitializer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_initView:(Landroid/view/View;)V:GetInitView_Landroid_view_View_Handler:Org.Aviran.CookieBar2.CookieBar/ICustomViewInitializerInvoker, CookieBar2\n" +
			"";
		mono.android.Runtime.register ("Org.Aviran.CookieBar2.CookieBar+CustomViewInitializer, CookieBar2", CookieBar_CustomViewInitializer.class, __md_methods);
	}


	public CookieBar_CustomViewInitializer ()
	{
		super ();
		if (getClass () == CookieBar_CustomViewInitializer.class)
			mono.android.TypeManager.Activate ("Org.Aviran.CookieBar2.CookieBar+CustomViewInitializer, CookieBar2", "", this, new java.lang.Object[] {  });
	}


	public void initView (android.view.View p0)
	{
		n_initView (p0);
	}

	private native void n_initView (android.view.View p0);

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
