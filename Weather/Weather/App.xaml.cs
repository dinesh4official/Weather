using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Weather.Views;
using Weather.Services;

namespace Weather
{
    [Preserve(AllMembers = true)]
    public partial class App : Application
    {
        #region Fields

        NetworkDetector networkDetector;

        #endregion

        public App()
        {
            InitializeComponent();
            networkDetector = new NetworkDetector();
            MainPage = new NavigationPage(new DashboardPage());
        }

        protected override void OnStart()
        {
            networkDetector.WireDetectorEvent();
        }

        protected override void OnSleep()
        {
            networkDetector.UnWireDetectorEvent();
        }

        protected override void OnResume()
        {
            networkDetector.WireDetectorEvent();
        }
    }
}
