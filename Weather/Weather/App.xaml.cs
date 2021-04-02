using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Weather.Views;

namespace Weather
{
    [Preserve(AllMembers = true)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DashboardPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
