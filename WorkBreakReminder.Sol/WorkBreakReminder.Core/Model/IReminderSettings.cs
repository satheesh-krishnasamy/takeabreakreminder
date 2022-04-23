using System.Collections.Generic;

namespace WorkBreakReminder.Core.Model
{
    public interface IReminderSettings
    {
        string MusicLocation { get; set; }
        ushort ReminderIntervalInMinutes { get; set; }
        bool MinimizeOnCloseWindow { get; set; }

        bool PopupWindowOnEachReminder { get; set; }
    }

    public interface IReminderSettingsReadOnly
    {
        string MusicLocation { get; }

        ushort ReminderIntervalInMinutes { get; }

        bool MinimizeOnCloseWindow { get; set; }

        bool PopupWindowOnEachReminder { get; set; }
    }

    public interface IReminderSettingsUpdateResult
    {
        bool IsUpdatedSuccessfully { get; }
        IEnumerable<string> Errors { get; }
    }
}