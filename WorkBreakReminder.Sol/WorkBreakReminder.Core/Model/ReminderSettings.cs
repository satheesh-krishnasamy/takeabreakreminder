using System.Collections.Generic;

namespace WorkBreakReminder.Core.Model
{
    public class ReminderSettings : IReminderSettings
    {
        public ReminderSettings() { }

        public ReminderSettings(
            string musicFileLocation,
            ushort reminderIntervalInMinutes,
            bool minimizeWindowOnClose,
            bool popupOnReminder)
        {
            this.MusicLocation = musicFileLocation;
            this.ReminderIntervalInMinutes = reminderIntervalInMinutes;
            this.MinimizeOnCloseWindow = minimizeWindowOnClose;
            this.PopupWindowOnEachReminder = popupOnReminder;
        }

        public string MusicLocation { get; set; }
        public ushort ReminderIntervalInMinutes { get; set; }

        public bool MinimizeOnCloseWindow { get; set; }

        public bool PopupWindowOnEachReminder { get; set; }

        public List<RecentFile> RecentReminderFiles { get; set; } = new List<RecentFile>();
    }
}