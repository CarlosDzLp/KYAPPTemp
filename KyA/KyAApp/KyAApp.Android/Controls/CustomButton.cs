using System;
using Android.Content;
using KyAApp.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Button),typeof(CustomButton))]
namespace KyAApp.Droid.Controls
{
    public class CustomButton : ButtonRenderer
    {
        public CustomButton(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.SetAllCaps(false);   
        }
    }
}
