using System;
using System.IO;
using System.Linq;
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
        public void SearchBarTextUpdated()
        {
            WeatherSearchBar.GetType().GetProperty("Text").SetValue(WeatherSearchBar, "New York");
            string searchbarValue = WeatherSearchBar.GetType().GetProperty("Text").GetValue(WeatherSearchBar) as string;
            app.Screenshot("SearchBar Text");
            Assert.IsTrue(searchbarValue == "New York");
        }
    }
}
