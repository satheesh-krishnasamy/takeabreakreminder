using System;
using System.Threading.Tasks;
using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.Logic
{
    public interface IReminderLogic : IDisposable
    {
        Task<bool> ResetToDefaultSettingsAsync();

        Task<ISaveSettingsResult> SaveSettingsAsync(IReminderSettingsReadOnly settingsToSave);

        bool Initialize();

        DateTime GetNextReminderTime();

        Task PlayReminderMusicAsync();
    }
}
