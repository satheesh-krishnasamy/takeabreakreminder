using System;

namespace WorkBreakReminder.Core.Model
{
    public class SaveSettingsResult : ISaveSettingsResult
    {
        public static readonly SaveSettingsResult Success = new SaveSettingsResult(true);

        public SaveSettingsResult(bool savedSuccessfully) : this(savedSuccessfully, null)
        {
        }

        public SaveSettingsResult(bool savedSuccessfully, Exception Error)
        {
            this.IsSavedSuccessfully = savedSuccessfully;
        }

        public bool IsSavedSuccessfully { get; set; }

        public Exception Error { get; }
    }
}
