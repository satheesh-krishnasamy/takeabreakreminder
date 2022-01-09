using System;
using WorkBreakReminder.Core.Model;
using WorkBreakReminder.Core.Utils;
using Xunit;


namespace WorkBreakReminder.Core.Tests.Storage
{
    public class UserProfileFileStorageTest
    {
        [Fact]
        public void Data_Should_Be_Stored_And_Retrieved_Succesfully()
        {
            var fileName = "test\\test.json";

            var storage = new UserProfileFileStorage();
            var dataToStore = new ReminderSettings()
            {
                MusicFilePath = "SomePath" + DateTime.UtcNow.Millisecond + ".file",
                ReminderTimeInMinutes = (ushort)(new Random().Next(10, 60))
            };

            storage.Save(dataToStore, fileName);

            var dataReadFromStorage = storage.Get(fileName);

            Assert.NotNull(dataReadFromStorage);
            Assert.Equal(dataToStore.MusicFilePath, dataReadFromStorage.MusicFilePath);
            Assert.Equal(dataToStore.ReminderTimeInMinutes, dataReadFromStorage.ReminderTimeInMinutes);
        }
    }
}
