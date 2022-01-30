using WorkBreakReminder.Core.Config;

namespace WorkBreakReminder.Core.Storage.Extensions.Config
{
    public class StorageConfig : IStorageConfig
    {
        public string PreferencesFileLocation { get; set; }
    }
}
