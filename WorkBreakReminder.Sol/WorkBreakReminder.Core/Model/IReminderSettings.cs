using System;
using System.Collections.Generic;
using System.IO;

namespace WorkBreakReminder.Core.Model
{
    public interface IReminderSettings : IReminderSettingsReadOnly
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

        bool MinimizeOnCloseWindow { get; }

        bool PopupWindowOnEachReminder { get; }

        List<RecentFile> RecentReminderFiles { get; }
    }

    public class RecentFile
    {
        private string filePath;

        public RecentFile()
        {

        }

        public RecentFile(string filePath)
        {
            this.FilePath = filePath;
            this.AccessedOn = DateTimeOffset.UnixEpoch.Ticks;
        }

        public string FilePath
        {
            get { return filePath; }
            set
            {
                FileName = Path.GetFileName(value);
                filePath = value;
            }
        }
        public long AccessedOn { get; set; }

        public string FileName { get; private set; }
    }

    public interface IReminderSettingsUpdateResult
    {
        bool IsUpdatedSuccessfully { get; }
        IEnumerable<string> Errors { get; }
    }
}