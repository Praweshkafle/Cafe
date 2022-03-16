using Acr.UserDialogs.Infrastructure;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LE.Cafe.Droid
{
    [Activity(Theme = "@style/LE.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }
        public override void OnBackPressed() { }
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            //Task startupWork = new Task(() => { SimulateStartup(); });
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        // Simulates background work that happens behind the splash screen
        //async void SimulateStartup()
        //{
        //    Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
        //    //await Task.Delay(8000); // Simulate a bit of startup work.
        //    Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
        //    StartActivity(new Intent(Application.Context, typeof(MainActivity)));}
    }
}