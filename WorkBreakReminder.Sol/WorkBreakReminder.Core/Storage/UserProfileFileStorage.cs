using System;
using System.IO;
using System.Text.Json;
using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core
{
    public class UserProfileFileStorage : IReminderStorage<ReminderSettings, string>
    {
        private readonly string profileFolderPath;

        public UserProfileFileStorage()
        {
            this.profileFolderPath = GetUserProfileFolder();
        }

        public ReminderSettings Get(string fileName)
        {
            var fileFullPath = Path.Combine(this.profileFolderPath, fileName);
            if (File.Exists(fileFullPath))
            {
                return JsonSerializer.Deserialize<ReminderSettings>(File.ReadAllText(fileFullPath));
            }

            return null;
        }

        public void Save(ReminderSettings data, string fileName)
        {
            var pathToWrite = Path.Combine(this.profileFolderPath, fileName);
            var directoryPath = Path.GetDirectoryName(pathToWrite);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllText(pathToWrite, JsonSerializer.Serialize(data));
        }

        private string GetUserProfileFolder()
        {
            string userProfileFolderPath;
            try
            {
                userProfileFolderPath = Environment.GetEnvironmentVariable("USERPROFILE");
                if (string.IsNullOrWhiteSpace(userProfileFolderPath))
                {
                    userProfileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                    if (string.IsNullOrWhiteSpace(userProfileFolderPath))
                    {
                        userProfileFolderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                    }
                }
            }
            catch
            {
                userProfileFolderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            }

            return userProfileFolderPath;
        }
    }
}
