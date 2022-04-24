using System;
using WorkBreakReminder.Core.Utils;
using Xunit;

namespace WorkBreakReminder.Core.Tests.Utils
{
    public class DateTimeUtilsTest
    {
        private const int TEST_VALUE_REMINDER_INTERVAL_IN_MINUTES = 10;

        [Fact]
        public void DateTimeUtils_Should_Produce_Time_For_Given_Minutes()
        {
            var expectedResultMin = DateTime.Now;
            var expectedResultMax = DateTime.Now.Date
                .AddHours(DateTime.Now.Hour)
                .AddMinutes(DateTime.Now.Minute)
                .AddMinutes(TEST_VALUE_REMINDER_INTERVAL_IN_MINUTES);

            var reminderDateTime = DateTimeUtils.GetNextReminderDateTime(TEST_VALUE_REMINDER_INTERVAL_IN_MINUTES, DateTime.Now);

            /* The reminder date time will always be <= the expected result.
             * Example if the reminder interval is 10 and the current time is 10:14 AM/PM then
             * the reminder time series will be 10:20, 10:30, 10:40, 10:50, 11:00..
             * Adding the 10 to current time will give us 10:24. Instead of reminding at 10:24,
             * this method will remind at 10:20 to make the time fall in the above time series.
             * So check for <= of the expected max time.
             * */
            Assert.True(reminderDateTime <= expectedResultMax && reminderDateTime > expectedResultMin);
        }
    }
}
