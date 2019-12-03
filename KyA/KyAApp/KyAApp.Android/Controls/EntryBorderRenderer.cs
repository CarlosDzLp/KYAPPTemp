using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using KyAApp.Controls;
using KyAApp.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryBorder), typeof(EntryBorderRenderer))]
namespace KyAApp.Droid.Controls
{
    public class EntryBorderRenderer : EntryRenderer
    {
        public EntryBorderRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                // creating gradient drawable for the curved background  
                var _gradientBackground = new GradientDrawable();
                //_gradientBackground.SetShape(ShapeType.Rectangle);
                //_gradientBackground.SetColor(Color.Black.ToAndroid());

                // Thickness of the stroke line  
                _gradientBackground.SetStroke(2, Color.Black.ToAndroid());

                // Radius for the curves  
                _gradientBackground.SetCornerRadius(
                    DpToPixels(this.Context, Convert.ToSingle(8)));

                // set the background of the   
                Control.SetBackground(_gradientBackground);

                // Set padding for the internal text from border  
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
