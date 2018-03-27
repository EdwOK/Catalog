using Android.App;
using Android.Content;
using Android.OS;

namespace Catalog.Droid
{
    [Activity(Theme = "@style/Splash", NoHistory = true, MainLauncher = true)]
    public class SplashActivity : global::Android.Support.V7.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InvokeMainActivity();
        }

        private void InvokeMainActivity()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}