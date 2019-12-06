using System.ComponentModel;
using System.Net;
using Android.Content;
using KyAApp.Controls;
using KyAApp.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPdf), typeof(CustomPdfRenderer))]
namespace KyAApp.Droid.Controls
{
    public class CustomPdfRenderer : WebViewRenderer
    {
        public CustomPdfRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomPdf;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl("https://drive.google.com/viewerng/viewer?embedded=true&url=" + customWebView.Uri);
            }
        }
    }
}
