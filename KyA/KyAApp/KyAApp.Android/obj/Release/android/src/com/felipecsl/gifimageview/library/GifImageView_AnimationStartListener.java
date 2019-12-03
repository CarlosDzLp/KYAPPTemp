package com.felipecsl.gifimageview.library;


public class GifImageView_AnimationStartListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.felipecsl.gifimageview.library.GifImageView.OnAnimationStart
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationStart:()V:GetOnAnimationStartHandler:Felipecsl.GifImageViewLib.GifImageView/IOnAnimationStartInvoker, GifImageView\n" +
			"";
		mono.android.Runtime.register ("Felipecsl.GifImageViewLib.GifImageView+AnimationStartListener, GifImageView", GifImageView_AnimationStartListener.class, __md_methods);
	}


	public GifImageView_AnimationStartListener ()
	{
		super ();
		if (getClass () == GifImageView_AnimationStartListener.class)
			mono.android.TypeManager.Activate ("Felipecsl.GifImageViewLib.GifImageView+AnimationStartListener, GifImageView", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationStart ()
	{
		n_onAnimationStart ();
	}

	private native void n_onAnimationStart ();

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
