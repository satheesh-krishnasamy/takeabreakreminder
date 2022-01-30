using System;
using System.IO;
using WorkBreakReminder.Core.Model;
using Xunit;


namespace WorkBreakReminder.Core.Storage.Extensions.Tests
{
    public class UserProfileFileStorageTest
    {
        [Fact]
        public void Data_Should_Be_Stored_And_Retrieved_Succesfully()
        {
            var fileName = Path.Combine("test", "test.json");

            var storage = new UserProfileFileStorage<ReminderSettings>();
            var dataToStore = new ReminderSettings()
            {
                MusicLocation = Path.Combine("SomePath", DateTime.UtcNow.Millisecond.ToString(), ".file"),
                ReminderIntervalInMinutes = (ushort)(new Random().Next(10, 60))
            };

            storage.Save(dataToStore, fileName);

            var dataReadFromStorage = storage.Get(fileName);

            Assert.NotNull(dataReadFromStorage);
            Assert.Equal(dataToStore.MusicLocation, dataReadFromStorage.MusicLocation);
            Assert.Equal(dataToStore.ReminderIntervalInMinutes, dataReadFromStorage.ReminderIntervalInMinutes);
        }
    }
}
