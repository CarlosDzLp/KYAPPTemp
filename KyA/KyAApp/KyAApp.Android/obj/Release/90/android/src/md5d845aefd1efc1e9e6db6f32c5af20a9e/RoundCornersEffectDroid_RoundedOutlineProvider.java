package md5d845aefd1efc1e9e6db6f32c5af20a9e;


public class RoundCornersEffectDroid_RoundedOutlineProvider
	extends android.view.ViewOutlineProvider
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getOutline:(Landroid/view/View;Landroid/graphics/Outline;)V:GetGetOutline_Landroid_view_View_Landroid_graphics_Outline_Handler\n" +
			"";
		mono.android.Runtime.register ("KyAApp.Droid.Effects.RoundCornersEffectDroid+RoundedOutlineProvider, KyAApp.Android", RoundCornersEffectDroid_RoundedOutlineProvider.class, __md_methods);
	}


	public RoundCornersEffectDroid_RoundedOutlineProvider ()
	{
		super ();
		if (getClass () == RoundCornersEffectDroid_RoundedOutlineProvider.class)
			mono.android.TypeManager.Activate ("KyAApp.Droid.Effects.RoundCornersEffectDroid+RoundedOutlineProvider, KyAApp.Android", "", this, new java.lang.Object[] {  });
	}

	public RoundCornersEffectDroid_RoundedOutlineProvider (float p0)
	{
		super ();
		if (getClass () == RoundCornersEffectDroid_RoundedOutlineProvider.class)
			mono.android.TypeManager.Activate ("KyAApp.Droid.Effects.RoundCornersEffectDroid+RoundedOutlineProvider, KyAApp.Android", "System.Single, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void getOutline (android.view.View p0, android.graphics.Outline p1)
	{
		n_getOutline (p0, p1);
	}

	private native void n_getOutline (android.view.View p0, android.graphics.Outline p1);

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
