package md5a6256f8d5bc17d3565a450e514d4a6e7;


public class MaterialContextThemeWrapper
	extends android.view.ContextThemeWrapper
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Xamarin.Forms.Material.Android.MaterialContextThemeWrapper, Xamarin.Forms.Material", MaterialContextThemeWrapper.class, __md_methods);
	}


	public MaterialContextThemeWrapper (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == MaterialContextThemeWrapper.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Material.Android.MaterialContextThemeWrapper, Xamarin.Forms.Material", "Android.Content.Context, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}

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
