using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.Config
{
    public interface IReminderConfiguration
    {
        public IStorageConfig Storage { get; }

        public IReminderSettingsReadOnly Default { get; }
    }
}
