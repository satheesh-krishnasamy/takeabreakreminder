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
        private DateTime dndDateTimeTill;

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
            var fromDateTime = DateTime.Now;
            if (DateTime.Now <= this.dndDateTimeTill)
            {
                fromDateTime = this.dndDateTimeTill;
            }

            return (int)((DateTimeUtils.GetNextReminderDateTime(this.reminderSettings.ReminderIntervalInMinutes, fromDateTime) - DateTime.Now).TotalMilliseconds);
        }

        private async void OnTimerTimeElapsedEventAsync(object? state)
        {
            await this.PlayReminderMusicAsync().ConfigureAwait(false);
            this.ResetTimerAndUpdateUI();

            UserNotificationPrefernces preferences = UserNotificationPrefernces.None;
            if (this.reminderSettings.PopupWindowOnEachReminder)
            {
                preferences = UserNotificationPrefernces.FocusWindow;
            }

            this.reminderView.OnReminder(new NotificationEventArgs(preferences));
        }

        private bool ValidateUserPreferences(ReminderSettings userPreferences)
        {
            return !(userPreferences == null
                || string.IsNullOrWhiteSpace(userPreferences.MusicLocation)
                || !reminderStorage.Validate(userPreferences.MusicLocation).IsValid
                || userPreferences.ReminderIntervalInMinutes < AppConstants.REMINDER_INTERVAL_MINIMUM
                || userPreferences.ReminderIntervalInMinutes > AppConstants.REMINDER_INTERVAL_MAXMUM);
        }

        private ReminderSettings GetDefaultSettingsInternal()
        {
            return new ReminderSettings()
            {
                MusicLocation = this.reminderConfiguration.Default.MusicLocation,
                ReminderIntervalInMinutes = this.reminderConfiguration.Default.ReminderIntervalInMinutes,
                MinimizeOnCloseWindow = this.reminderConfiguration.Default.MinimizeOnCloseWindow,
                PopupWindowOnEachReminder = this.reminderConfiguration.Default.PopupWindowOnEachReminder
            };
        }

        private async Task<IReminderSettingsReadOnly> loadSettingsAsync()
        {
            try
            {
                if (reminderSettings != null)
                {
                    return reminderSettings;
                }

                // Get the reminder settings from storage
                reminderSettings = await this.reminderStorage.GetAsync(reminderConfiguration.Storage.PreferencesFileLocation);

                // Validate the settings.
                if (!this.ValidateUserPreferences(reminderSettings))
                {
                    reminderSettings = this.GetDefaultSettingsInternal();
                }

                return reminderSettings;
            }
            catch
            {
                // Load the default settings value in case of unresolvable error.
                reminderSettings = this.GetDefaultSettingsInternal();
                return reminderSettings;
            }
        }

        public async Task<IReminderSettingsReadOnly> GetCurrentSettingsAsync()
        {
            return await this.loadSettingsAsync();
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


        public DateTime GetNextReminderTime()
        {
            var fromDateTime = DateTime.Now;
            if (DateTime.Now <= this.dndDateTimeTill)
            {
                fromDateTime = this.dndDateTimeTill;
            }
            return DateTimeUtils.GetNextReminderDateTime(this.reminderSettings.ReminderIntervalInMinutes, fromDateTime);
        }

        /// <summary>
        /// Saves the application settings.
        /// </summary>
        /// <param name="settingsToSave">Settings object to be saved.</param>
        /// <returns>Task representing the Save operation.</returns>
        public async Task<ISaveSettingsResult> SaveSettingsAsync(IReminderSettingsReadOnly settingsToSave)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                bool shouldSaveTimerInterval = settingsToSave.ReminderIntervalInMinutes > 0
                    && this.reminderSettings.ReminderIntervalInMinutes != settingsToSave.ReminderIntervalInMinutes;

                if (shouldSaveTimerInterval)
                {
                    this.reminderSettings.ReminderIntervalInMinutes = settingsToSave.ReminderIntervalInMinutes;
                }
                else if (settingsToSave.ReminderIntervalInMinutes < 1)
                {
                    errors.AppendLine("Reminder interval in minutes is invalid.");
                }

                // Save the following 2 preferences all the time.
                // For other options that do not require resetting the reminder timer or ui, 
                // Just save the changes.
                this.reminderSettings.PopupWindowOnEachReminder = settingsToSave.PopupWindowOnEachReminder;
                this.reminderSettings.MinimizeOnCloseWindow = settingsToSave.MinimizeOnCloseWindow;

                var shouldSaveMusicLocation = this.reminderSettings.MusicLocation != settingsToSave.MusicLocation;
                if (shouldSaveMusicLocation)
                {
                    var musicLocationValidationResult = this.reminderStorage.Validate(settingsToSave.MusicLocation);
                    if (musicLocationValidationResult.IsValid)
                    {
                        this.reminderSettings.MusicLocation = settingsToSave.MusicLocation;
                    }
                    else
                    {
                        shouldSaveMusicLocation = false;
                        errors.AppendLine("Selected music location is invalid.");
                    }
                }

                await this.reminderStorage.SaveAsync(this.reminderSettings, this.reminderConfiguration.Storage.PreferencesFileLocation);

                if (shouldSaveMusicLocation || shouldSaveTimerInterval)
                {
                    this.ResetTimerAndUpdateUI();

                    if (shouldSaveMusicLocation)
                    {
                        await this.PlayReminderMusicAsync().ConfigureAwait(false);
                    }
                }

                //else if (shouldSaveTimerInterval)
                //{
                //    this.ResetTimerTimeout();
                //    this.reminderView.BeforeInitViewCallback();
                //    this.SetReminderSummary();
                //    this.reminderView.AfterInitViewCallback();
                //}
                

                if (errors.Length > 0)
                    return new SaveSettingsResult(false, new Exception(errors.ToString()));

                return SaveSettingsResult.Success;
            }
            catch (Exception exp)
            {
                return new SaveSettingsResult(false, exp);
            }
        }

        /// <summary>
        /// Method to initialize the view and other master data.
        /// </summary>
        /// <returns>True if initialized correctly; false - otherwise.</returns>
        public async Task<bool> InitializeAsync()
        {
            await this.loadSettingsAsync();
            this.ResetTimerAndUpdateUI();
            return true;
        }

        public async Task<bool> DoNotDisturbForAnHourAsync()
        {
            this.dndDateTimeTill = DateTime.Now.AddHours(1);
            this.ResetTimerAndUpdateUI();
            return true;
        }

        public async Task<bool> CancelDNDAsync()
        {
            this.dndDateTimeTill = DateTime.Now.AddHours(-1);
            this.ResetTimerAndUpdateUI();
            return true;
        }

        /// <summary>
        /// Disposes the applicable internal unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.reminderTriggerTimer != null)
            {
                try
                {
                    this.reminderTriggerTimer.Change(int.MaxValue, Timeout.Infinite);
                    this.reminderTriggerTimer.Dispose();
                }
                catch
                {
                    // cannot do anything; just exit.
                }
            }
        }
    }
}
