using System;
using Xamarin.Forms;
using KyAApp.Controls;
using KyAApp.Droid.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Android.Support.V4.Content;
using Android.Graphics.Drawables;

[assembly:ExportRenderer(typeof(GifImageView),typeof(GifImageViewRenderer))]
namespace KyAApp.Droid.Controls
{
    public class GifImageViewRenderer : ViewRenderer<Image,Felipecsl.GifImageViewLib.GifImageView>
    {
        public GifImageViewRenderer(Context context) : base(context)
        {
        }
        public static void Init() { }

        Felipecsl.GifImageViewLib.GifImageView gif;

        GifImageView gifImage = null;
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            gifImage = (GifImageView)Element;
            gif = new Felipecsl.GifImageViewLib.GifImageView(Forms.Context);

            SetNativeControl(gif);
        }
        protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
                try
                {
                    gif.StopAnimation();
                    //gif.SetBytes(bytes);
                    gif.SetImageDrawable(GetDrawable(gifImage.Image));
                    gif.StartAnimation();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to load gif: " + ex.Message);
                }

            
        }
        private Drawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            return drawable;
        }
    }
}
