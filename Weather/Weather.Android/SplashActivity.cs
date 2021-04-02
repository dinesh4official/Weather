using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace Weather.Droid
{
    [Preserve(AllMembers = true)]
    [Activity(Label = "Weather", MainLauncher = true, Theme = "@style/splashscreen", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        #region Override Methods

        protected async override void OnResume()
        {
            base.OnResume();
            await StartupApp();
        }

        public override void OnBackPressed() { }

        #endregion

        #region Private Methods


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