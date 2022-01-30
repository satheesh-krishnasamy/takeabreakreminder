namespace WorkBreakReminder.Core.Model
{
    public class ReminderSettings : IReminderSettingsReadOnly, IReminderSettings
    {
        public ReminderSettings() { }

        public ReminderSettings(string musicFileLocation, ushort reminderIntervalInMinutes)
        {
            this.MusicLocation = musicFileLocation;
            this.ReminderIntervalInMinutes = reminderIntervalInMinutes;
        }

        public string MusicLocation { get; set; }
        public ushort ReminderIntervalInMinutes { get; set; }
    }
}