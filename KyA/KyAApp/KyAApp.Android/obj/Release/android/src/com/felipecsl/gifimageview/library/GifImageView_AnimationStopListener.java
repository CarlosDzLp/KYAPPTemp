package com.felipecsl.gifimageview.library;


public class GifImageView_AnimationStopListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.felipecsl.gifimageview.library.GifImageView.OnAnimationStop
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationStop:()V:GetOnAnimationStopHandler:Felipecsl.GifImageViewLib.GifImageView/IOnAnimationStopInvoker, GifImageView\n" +
			"";
		mono.android.Runtime.register ("Felipecsl.GifImageViewLib.GifImageView+AnimationStopListener, GifImageView", GifImageView_AnimationStopListener.class, __md_methods);
	}


	public GifImageView_AnimationStopListener ()
	{
		super ();
		if (getClass () == GifImageView_AnimationStopListener.class)
			mono.android.TypeManager.Activate ("Felipecsl.GifImageViewLib.GifImageView+AnimationStopListener, GifImageView", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationStop ()
	{
		n_onAnimationStop ();
	}

	private native void n_onAnimationStop ();

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
