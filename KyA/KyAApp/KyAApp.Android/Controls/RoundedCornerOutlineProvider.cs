using System;
using Android.Graphics;
using Android.Views;
using KyAApp.Controls;

namespace KyAApp.Droid.Controls
{
	public class RoundedCornerOutlineProvider : ViewOutlineProvider
	{
		private readonly CanvasView _pancake;
		private readonly Func<double, float> _convertToPixels;

		public RoundedCornerOutlineProvider(CanvasView pancake, Func<double, float> convertToPixels)
		{
			_pancake = pancake;
			_convertToPixels = convertToPixels;

		}

		public override void GetOutline(global::Android.Views.View view, Outline outline)
		{
			if (_pancake.Sides != 4)
			{
				var cornerRadius = (view.Width / _pancake.Width) * _pancake.CornerRadius.TopLeft;
				var hexPath = PolygonUtils.GetPolygonCornerPath(view.Width, view.Height, _pancake.Sides, cornerRadius, _pancake.OffsetAngle);
				if (hexPath.IsConvex)
				{
					outline.SetConvexPath(hexPath);
				}

				return;
			}

			// TODO: Figure out how to clip individual rounded corners with different radii.
			outline.SetRoundRect(new Rect(0, 0, view.Width, view.Height), _convertToPixels(_pancake.CornerRadius.TopLeft));
		}
	}
}
