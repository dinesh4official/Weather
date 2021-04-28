using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Weather.UITests
{
#if Android
    [TestFixture(Platform.Android)]
#else
    [TestFixture(Platform.iOS)]
#endif
    public class Tests
    {
        IApp app;
        Platform platform;

        static readonly Func<AppQuery, AppQuery> WeatherSearchBar = c => c.Marked("WeatherSearchBar");
        static readonly Func<AppQuery, AppQuery> WeatherGetReportButton = c => c.Marked("WeatherGetReport");

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
#if Android
            app = AppInitializer.StartApp(Platform.Android);
#else
            app = AppInitializer.StartApp(Platform.iOS);
#endif
        }

        [Test]
        [Description("Check whether the SearchBar text is updated")]
        public void SearchBarTextUpdated()
        {
            app.EnterText(WeatherSearchBar, "New York");
        }

        [Test]
        [Description("Check whether the SearchBar button is clicked")]
        public void SearchBarButtonIsClicked()
        {
            app.Tap(WeatherGetReportButton);
            app.Screenshot("No_Items");
        }
    }
}
