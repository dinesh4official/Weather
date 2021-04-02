using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;

namespace Weather.Droid
{
    [Activity(Label = "Weather", MainLauncher = true, Theme = "@style/splashscreen", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        #region Override Methods

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            this.InitializView(savedInstanceState);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.InitializView(savedInstanceState);
        }

        protected async override void OnResume()
        {
            base.OnResume();
            await StartupApp();
        }

        public override void OnBackPressed() { }

        #endregion

        #region Private Methods

        private void InitializView(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.SplashScreen);
        }

        private async Task StartupApp()
        {
            await Task.Delay(100);
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        #endregion
    }
}