using System.Collections.Generic;

namespace WorkBreakReminder.Core.Model
{
    public interface IReminderSettings
    {
        string MusicLocation { get; set; }
        ushort ReminderIntervalInMinutes { get; set; }
    }

    public interface IReminderSettingsReadOnly
    {
        string MusicLocation { get; }

        ushort ReminderIntervalInMinutes { get; }
    }

    public interface IReminderSettingsUpdateResult
    {
        bool IsUpdatedSuccessfully { get; }
        IEnumerable<string> Errors { get; }
    }
}