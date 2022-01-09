using System;
using WorkBreakReminder.Core.Model;
using WorkBreakReminder.Core.Utils;
using Xunit;

namespace WorkBreakReminder.Core.Tests
{
    public class ReminderSettingsTests
    {
        private const string TEST_VALUE_MUSIC_PATH = "PathString";
        private const int TEST_VALUE_REMINDER_TIME_IN_MINUTES = 10;

        [Fact]
        public void Model_Object_Should_Set_Get_Correctly()
        {
            var modelObj = new ReminderSettings();

            Assert.Equal(default(string), modelObj.MusicFilePath);
            Assert.Equal(0, modelObj.ReminderTimeInMinutes);

            modelObj.MusicFilePath = TEST_VALUE_MUSIC_PATH;
            modelObj.ReminderTimeInMinutes = TEST_VALUE_REMINDER_TIME_IN_MINUTES;

            Assert.Equal(TEST_VALUE_MUSIC_PATH, modelObj.MusicFilePath);
            Assert.Equal(TEST_VALUE_REMINDER_TIME_IN_MINUTES, modelObj.ReminderTimeInMinutes);
        }
    }
}
