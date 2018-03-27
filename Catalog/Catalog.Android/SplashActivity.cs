using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Catalog.Droid
{
    [Activity(Label = "Catalog", Icon = "@drawable/icon", Theme = "@style/Theme.Splash", NoHistory = true, MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            InvokeMainActivity();
        }

        private void InvokeMainActivity()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}