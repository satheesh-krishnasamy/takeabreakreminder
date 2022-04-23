using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.Storage.Extensions
{
    /// <summary>
    /// Class implements the IReminderStorage using the user's default directory as target location.
    /// </summary>
    /// <typeparam name="TUserProfileData">Class instance represents the data to be persisted into the storage.</typeparam>
    public class UserProfileFileStorage<TUserProfileData> : IReminderStorage<TUserProfileData, string>
    {
        private readonly string profileFolderPath;

        /// <summary>
        /// Initialized the <see cref="UserProfileFileStorage"/> using 
        /// the current user directory in local disk as target location.
        /// </summary>
        public UserProfileFileStorage()
        {
            this.profileFolderPath = GetUserProfileFolder();
        }


        public async Task<TUserProfileData> GetAsync(string fileName)
        {
            var fileFullPath = Path.Combine(this.profileFolderPath, fileName);
            if (File.Exists(fileFullPath))
            {
                return JsonSerializer.Deserialize<TUserProfileData>(await File.ReadAllTextAsync(fileFullPath));
            }

            return default(TUserProfileData);
        }

        public async Task<bool> SaveAsync(TUserProfileData data, string fileName)
        {
            var pathToWrite = Path.Combine(this.profileFolderPath, fileName);
            var directoryPath = Path.GetDirectoryName(pathToWrite);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            await File.WriteAllTextAsync(pathToWrite, JsonSerializer.Serialize(data));
            return true;
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
