using System;
using System.Threading.Tasks;
using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.Logic
{
    /// <summary>
    /// Class implementing the reminder logic.
    /// </summary>
    public interface IReminderLogic : IDisposable
    {
        /// <summary>
        /// Method to be called to reset the reminder settings to default values.
        /// </summary>
        /// <returns></returns>
        Task<bool> ResetToDefaultSettingsAsync();

        /// <summary>
        /// Method to be called to save the given reminder settings.
        /// </summary>
        /// <param name="settingsToSave">Reminder settings instance to be saved.</param>
        /// <returns>Status of the save operation.</returns>
        Task<ISaveSettingsResult> SaveSettingsAsync(IReminderSettingsReadOnly settingsToSave);

        /// <summary>
        /// Method to be called to initialize the logic.
        /// </summary>
        /// <returns>Status of initialize operation. true - if successfully; false otherwise.</returns>
        Task<bool> InitializeAsync();

        /// <summary>
        /// Gets the date time when the next reminder is going to happen.
        /// </summary>
        /// <returns></returns>
        DateTime GetNextReminderTime();

        /// <summary>
        /// Method to be called when the view wants to play the reminder notification music.
        /// </summary>
        /// <returns></returns>
        Task PlayReminderMusicAsync();

        Task<IReminderSettingsReadOnly> GetCurrentSettingsAsync();

        /// <summary>
        /// To ask the reminder app to not to disturb for an hour from now.      
        /// </summary>
        /// <remarks>As of now, calling this method will cause the app to not to play the reminder music
        /// for an hour from now.</remarks>
        /// <returns>true - if the request got processed successfully; false - otherwise.</returns>
        Task<bool> DoNotDisturbForAnHourAsync();

        /// <summary>
        /// Resumes the reminder functionality that was paused for an hour earlier.
        /// </summary>
        /// <returns></returns>
        Task<bool> CancelDNDAsync();

        bool ResetReminder();
    }
}
