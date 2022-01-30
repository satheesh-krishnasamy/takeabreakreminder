using WorkBreakReminder.Core.Config;
using WorkBreakReminder.Core.Model;
using WorkBreakReminder.Core.Storage.Extensions.Config;

namespace WorkBreakReminder.Config
{
    public class AppSettings
    {
        public StorageConfig Storage { get; set; }

        public ReminderSettings Default { get; set; }
    }

    public class AppSettingsReadonly : IReminderConfiguration
    {
        private readonly AppSettings appSettings;

        public AppSettingsReadonly(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public IStorageConfig Storage
        {
            get
            {
                return this.appSettings.Storage;
            }
        }

        public IReminderSettingsReadOnly Default
        {
            get
            {
                return this.appSettings.Default;
            }
        }
    }

}
