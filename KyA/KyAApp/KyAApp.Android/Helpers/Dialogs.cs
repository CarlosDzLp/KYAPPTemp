using System;
using System.Threading.Tasks;
using Android.App;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Dmax.Dialog;
using KyAApp.Droid.Helpers;
using KyAApp.Helpers;
using Org.Aviran.CookieBar2;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:Dependency(typeof(Dialogs))]
namespace KyAApp.Droid.Helpers
{
    public class Dialogs : IDialogs
    {
        AlertDialog dialogAlert = null;
        public async Task Hide()
        {
            dialogAlert.Dismiss();
        }

        public async Task Show(string message)
        {
            dialogAlert = new SpotsDialog.Builder().SetContext(MainActivity.Current).SetMessage(message).SetCancelable(false)
                .Build();
            dialogAlert.Show();
        }

        public async Task SnackBarError(string message,string title, TypeSnackBar typeSnackBar)
        {
            if (typeSnackBar == TypeSnackBar.Bottom)
            {
                Activity activity = CrossCurrentActivity.Current.Activity;
                Android.Views.View activityRootView = activity.FindViewById(Android.Resource.Id.Content);
                Android.Support.Design.Widget.Snackbar snackBar = Android.Support.Design.Widget.Snackbar.Make(activityRootView, message, Snackbar.LengthLong);
                snackBar.SetActionTextColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
                Android.Views.View snackbarview = snackBar.View;
                snackbarview.SetBackgroundResource(Resource.Drawable.snackBackground);
                ViewGroup.MarginLayoutParams paramss = (ViewGroup.MarginLayoutParams)snackbarview.LayoutParameters;
                paramss.SetMargins(30, 0, 30, 40);
                snackbarview.SetBackground(
                    MainActivity.Current.ApplicationContext.GetDrawable(Resource.Drawable.snackBarError));
                snackBar.Show();
            }
            else if (typeSnackBar == TypeSnackBar.Top)
            {
                CookieBar.Build(MainActivity.Current)
                    .SetIcon(Resource.Drawable.ic_error)
                .SetTitle(title)
                .SetIconAnimation(Resource.Animator.iconspin)
                .SetBackgroundColor(Resource.Color.backgroundcoockieerror)
                .SetTitleColor(Resource.Color.cookiebartitle)
                .SetMessageColor(Resource.Color.cookiebartitle)
                .SetMessage(message)
                .SetEnableAutoDismiss(true)
                .SetSwipeToDismiss(true)
                .Show();
            }
        }


        public async Task SnackBarSucces(string message,string title, TypeSnackBar typeSnackBar)
        {
            if (typeSnackBar == TypeSnackBar.Bottom)
            {
                Activity activity = CrossCurrentActivity.Current.Activity;
                Android.Views.View activityRootView = activity.FindViewById(Android.Resource.Id.Content);
                Android.Support.Design.Widget.Snackbar snackBar = Android.Support.Design.Widget.Snackbar.Make(activityRootView, message, Snackbar.LengthLong);
                snackBar.SetActionTextColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
                Android.Views.View snackbarview = snackBar.View;
                snackbarview.SetBackgroundResource(Resource.Drawable.snackBackground);
                ViewGroup.MarginLayoutParams paramss = (ViewGroup.MarginLayoutParams)snackbarview.LayoutParameters;
                paramss.SetMargins(30, 0, 30, 40);
                snackbarview.LayoutParameters = paramss;
                snackbarview.SetBackground(
                    MainActivity.Current.ApplicationContext.GetDrawable(Resource.Drawable.snackBackground));
                snackBar.Show();
            }
            else if (typeSnackBar == TypeSnackBar.Top)
            {
                CookieBar.Build(MainActivity.Current)
                    .SetIcon(Resource.Drawable.ic_done)
                .SetTitle(title)
                .SetIconAnimation(Resource.Animator.iconspin)
                .SetBackgroundColor(Resource.Color.backgroundcoockiesuccess)
                .SetTitleColor(Resource.Color.cookiebartitle)
                .SetMessageColor(Resource.Color.cookiebartitle)
                .SetMessage(message)
                .SetEnableAutoDismiss(true)
                .SetSwipeToDismiss(true)
                .Show();
            }
        }

        public void Toast(string message)
        {
            var toast = Android.Widget.Toast.MakeText(MainActivity.Current, message, Android.Widget.ToastLength.Short);
            toast.Show();
        }
    }
}
