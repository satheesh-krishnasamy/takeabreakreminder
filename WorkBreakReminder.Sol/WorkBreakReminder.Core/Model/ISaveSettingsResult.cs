using System;

namespace WorkBreakReminder.Core.Model
{
    public interface ISaveSettingsResult
    {
        bool IsSavedSuccessfully { get; }

        Exception Error { get; }
    }
}