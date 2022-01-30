using System;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkBreakReminder.Core.Config;
using WorkBreakReminder.Core.Constants;
using WorkBreakReminder.Core.Model;
using WorkBreakReminder.Core.Utils;
using WorkBreakReminder.Core.View;

namespace WorkBreakReminder.Core.Logic
{
    public sealed class ReminderLogic : IReminderLogic
    {
        private readonly IReminderView reminderView;
        private readonly IReminderConfiguration reminderConfiguration;
        private readonly IReminderStorage<ReminderSettings, string> reminderStorage;
        private readonly Timer reminderTriggerTimer;

        private ReminderSettings reminderSettings;
        private ReminderSettings defaultReminderSettings = null;

        public ReminderLogic(
            IReminderView reminderView,
            IReminderStorage<ReminderSettings, string> reminderStorage,
            IReminderConfiguration reminderConfiguration)
        {
            this.reminderView = reminderView;
            this.reminderConfiguration = reminderConfiguration;
            this.reminderStorage = reminderStorage;
            this.reminderTriggerTimer = new Timer(OnTimerTimeElapsedEventAsync, null, int.MaxValue, Timeout.Infinite);
        }

        public DateTime GetNextReminderTime()
        {
            return DateTimeUtils.GetNextReminderDateTime(this.reminderSettings.ReminderIntervalInMinutes);
        }

        public IReminderSettingsReadOnly LoadSettings()
        {
            try
            {
                if (reminderSettings != null)
                {
                    return reminderSettings;
                }

                reminderSettings = this.reminderStorage.Get(reminderConfiguration.Storage.PreferencesFileLocation);
                if (!this.ValidateUserPreferences(reminderSettings))
                {
                    reminderSettings = this.GetDefaultSettingsInternal();
                }

                return reminderSettings;
            }
            catch
            {
                reminderSettings = this.GetDefaultSettingsInternal();
                return reminderSettings;
            }
        }

        public async Task<ISaveSettingsResult> SaveSettingsAsync(IReminderSettingsReadOnly settingsToSave)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                bool timerIntervalChanged = settingsToSave.ReminderIntervalInMinutes > 0
                    && this.reminderSettings.ReminderIntervalInMinutes != settingsToSave.ReminderIntervalInMinutes;
                if (timerIntervalChanged)
                {
                    this.reminderSettings.ReminderIntervalInMinutes = settingsToSave.ReminderIntervalInMinutes;
                }
                else if (settingsToSave.ReminderIntervalInMinutes < 1)
                {
                    errors.AppendLine("Reminder interval in minutes is invalid.");
                }

                var musicLocationChanged = this.reminderSettings.MusicLocation != settingsToSave.MusicLocation;
                if (musicLocationChanged)
                {
                    var musicLocationValidationResult = this.reminderStorage.Validate(settingsToSave.MusicLocation);
                    if (musicLocationValidationResult.IsValid)
                    {
                        this.reminderSettings.MusicLocation = settingsToSave.MusicLocation;
                    }
                    else
                    {
                        musicLocationChanged = false;
                        errors.AppendLine("Selected music location is invalid.");
                    }
                }

                if (musicLocationChanged || timerIntervalChanged)
                {
                    this.reminderStorage.Save(this.reminderSettings, this.reminderConfiguration.Storage.PreferencesFileLocation);

                    if (musicLocationChanged && timerIntervalChanged)
                    {
                        this.ResetTimerAndUpdateUI();
                    }
                    else if (timerIntervalChanged)
                    {
                        this.ResetTimerTimeout();
                        this.reminderView.BeforeInitViewCallback();
                        this.SetReminderSummary();
                        this.reminderView.AfterInitViewCallback();
                    }
                    else
                    {
                        await this.PlayReminderMusicAsync().ConfigureAwait(false);
                    }

                    return SaveSettingsResult.Success;
                }

                return new SaveSettingsResult(false, new Exception(errors.ToString()));
            }
            catch (Exception exp)
            {
                return new SaveSettingsResult(false, exp);
            }
        }

        public bool Initialize()
        {
            this.LoadSettings();
            this.ResetTimerAndUpdateUI();
            return true;
        }

        private void ResetTimerAndUpdateUI()
        {
            this.ResetTimerTimeout();

            this.reminderView.BeforeInitViewCallback();
            this.reminderView.UpdateUIWithReminderSettings(this.reminderSettings);
            this.SetReminderSummary();
            this.reminderView.AfterInitViewCallback();
        }

        private void SetReminderSummary()
        {
            var nextReminderDateTime = this.GetNextReminderTime();
            var reminderSummary = $"Next reminder is at {nextReminderDateTime.ToString("hh:mm tt", System.Globalization.CultureInfo.CurrentCulture)}.";
            this.reminderView.SetNextReminderTime(reminderSummary);
        }

        private void ResetTimerTimeout()
        {
            var alarmTimeFromNow = CalculateTimerInterval();
            reminderTriggerTimer.Change(alarmTimeFromNow, Timeout.Infinite);
        }

        private int CalculateTimerInterval()
        {
            return (int)((DateTimeUtils.GetNextReminderDateTime(this.reminderSettings.ReminderIntervalInMinutes) - DateTime.Now).TotalMilliseconds);
        }

        private async void OnTimerTimeElapsedEventAsync(object? state)
        {
            await this.PlayReminderMusicAsync().ConfigureAwait(false);

            this.ResetTimerAndUpdateUI();
        }

        public async Task PlayReminderMusicAsync()
        {
            await Task.Run(() =>
            {
                using (var soundPlayer = new SoundPlayer(this.reminderSettings.MusicLocation))
                {
                    soundPlayer.Play(); // can also use soundPlayer.PlaySync()
                }
            });
        }

        private bool ValidateUserPreferences(ReminderSettings userPreferences)
        {
            return !(userPreferences == null
                || string.IsNullOrWhiteSpace(userPreferences.MusicLocation)
                || !reminderStorage.Validate(userPreferences.MusicLocation).IsValid
                || userPreferences.ReminderIntervalInMinutes < AppConstants.REMINDER_INTERVAL_MINIMUM
                || userPreferences.ReminderIntervalInMinutes > AppConstants.REMINDER_INTERVAL_MAXMUM);
        }

        public async Task<bool> ResetToDefaultSettingsAsync()
        {
            var defaultSettings = GetDefaultSettingsInternal();
            var saveResult = await this.SaveSettingsAsync(defaultSettings);
            if (saveResult.IsSavedSuccessfully)
            {
                this.ResetTimerAndUpdateUI();
                return true;
            }
            else
            {
                return false;
            }
        }

        private ReminderSettings GetDefaultSettingsInternal()
        {
            return new ReminderSettings()
            {
                MusicLocation = this.reminderConfiguration.Default.MusicLocation,
                ReminderIntervalInMinutes = this.reminderConfiguration.Default.ReminderIntervalInMinutes
            };
        }

        public void Dispose()
        {
            try
            {
                this.SaveSettingsAsync(this.reminderSettings);
            }
            catch
            {

            }

            if (this.reminderTriggerTimer != null)
            {
                try
                {
                    this.reminderTriggerTimer.Change(int.MaxValue, Timeout.Infinite);
                    this.reminderTriggerTimer.Dispose();
                }
                catch
                {

                }
            }
        }
    }
}
