
using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Com.OneSignal;
using FormsToolkit;
using ImageCircle.Forms.Plugin.Droid;
using KyAApp.Helpers;
using Lottie.Forms.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

namespace KyAApp.Droid
{
    [Activity
        (
            Label = "KyAApp",
            Icon = "@mipmap/icon",
            Theme = "@style/MainTheme",
            MainLauncher = false,
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Current { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Current = this;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental", "FastRenderers_Experimental");
            Forms.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();
            ImageCircleRenderer.Init();
            OneSignal.Current.StartInit("c2df8f3d-8733-47b2-87b3-9787310cecc3")
                  .EndInit();
            GetPermissions();          
            LoadApplication(new App());
            MessagingService.Current.Subscribe<bool>(MessageKeys.StatusBar, (args, sender) =>
            {
                if (sender)
                {
                    setLightStatusBar(this, Android.Graphics.Color.Transparent);
                }
                else
                {
                    ClearLightStatusBar(this, Android.Graphics.Color.ParseColor("#0CB392"));
                }
            });
        }

        public void setLightStatusBar(Activity activity,Android.Graphics.Color color)
        {
            int flags = (int)activity.Window.DecorView.SystemUiVisibility; // get current flag
            flags |= (int)SystemUiFlags.LightStatusBar;   // add LIGHT_STATUS_BAR to flag
            activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)flags;
            activity.Window.SetStatusBarColor(color);
        }
        public void ClearLightStatusBar(Activity activity, Android.Graphics.Color color)
        {
            int newUiVisibility = (int)activity.Window.DecorView.SystemUiVisibility;
            newUiVisibility &= ~(int)Android.Views.SystemUiFlags.LightStatusBar;
            activity.Window.DecorView.SystemUiVisibility = (Android.Views.StatusBarVisibility)newUiVisibility;
            activity.Window.SetStatusBarColor(color);
        }


        private void GetPermissions()
        {
            try
            {
                ActivityCompat.RequestPermissions(this, new string[]
                {
                    Manifest.Permission.ModifyAudioSettings,
                    Manifest.Permission.ReadExternalStorage,
                    Manifest.Permission.WriteExternalStorage,
                    Manifest.Permission.RecordAudio,
                    Manifest.Permission.AccessMockLocation,              
                }, 0);
            }
            catch
            {
                
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                //Debug.WriteLine("Android back button: There are some pages in the PopupStack");
            }
            else
            {
                //Debug.WriteLine("Android back button: There are not any pages in the PopupStack");
            }
        }
    }
}