using System;
using Xamarin.UITest.Utils;

namespace Weather.UITests
{
    public class WaitTimes : IWaitTimes
    {
        public TimeSpan GestureWaitTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }
        public TimeSpan WaitForTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }

        public TimeSpan GestureCompletionTimeout => new TimeSpan(0, 0, 1, 0);
    }
}
