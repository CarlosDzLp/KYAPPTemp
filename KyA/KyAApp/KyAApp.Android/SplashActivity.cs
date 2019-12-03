using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KyAApp.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/MyTheme.Splash")]
    public class SplashActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            SimulateStartup();
            //Task startupWork = new Task(() => { SimulateStartup(); });
            //startupWork.Start();
        }
        async void SimulateStartup()
        {
            await Task.Delay(2000); // Simulate a bit of startup work.
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}