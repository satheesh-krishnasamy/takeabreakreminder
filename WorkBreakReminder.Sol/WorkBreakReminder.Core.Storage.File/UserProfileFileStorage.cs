using System;
using System.IO;
using System.Text.Json;
using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.Storage.Extensions
{
    public class UserProfileFileStorage<TUserProfileData> : IReminderStorage<TUserProfileData, string>
    {
        private readonly string profileFolderPath;

        public UserProfileFileStorage()
        {
            this.profileFolderPath = GetUserProfileFolder();
        }

        public TUserProfileData Get(string fileName)
        {
            var fileFullPath = Path.Combine(this.profileFolderPath, fileName);
            if (File.Exists(fileFullPath))
            {
                return JsonSerializer.Deserialize<TUserProfileData>(File.ReadAllText(fileFullPath));
            }

            return default(TUserProfileData);
        }

        public void Save(TUserProfileData data, string fileName)
        {
            var pathToWrite = Path.Combine(this.profileFolderPath, fileName);
            var directoryPath = Path.GetDirectoryName(pathToWrite);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllText(pathToWrite, JsonSerializer.Serialize(data));
        }

        public IStorageValidationResult Validate(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return new StorageValidationResult(false, new string[] { "Invalid file name" });

            var pathToWrite = Path.Combine(this.profileFolderPath, fileName);
            return File.Exists(pathToWrite) ? StorageValidationResult.Success : new StorageValidationResult(false, new string[] { "File does not exists." });
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
