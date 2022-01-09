using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WorkBreakReminder.Core;
using WorkBreakReminder.Core.Constants;
using WorkBreakReminder.Core.Model;
using WorkBreakReminder.Core.Utils;

namespace WorkBreakReminder
{
    public partial class mainForm : Form
    {
        private readonly string USER_PREFERENCES_FILENAME = Path.Combine("workbreakreminder", "preferences.json");
        private const string MUSIC_FULL_PATH_DEFAULT = @"c:\Windows\Media\chimes.wav";

        private readonly IReminderStorage<ReminderSettings, string> storage;

        private readonly System.Timers.Timer reminderTriggerTimer1;
        private ReminderSettings defaultReminderSettings = null;

        private ReminderSettings reminderSettings;

        public mainForm()
        {
            InitializeComponent();

            this.storage = new UserProfileFileStorage();

            this.reminderTriggerTimer1 = new System.Timers.Timer();
            this.reminderTriggerTimer1.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerTimeElapsedEventAsync);

            this.LoadSettings();
            this.SetUIWithSettings();
        }

        private delegate void UpdateControlsDelegate();

        private void SetUIWithSettings()
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateControlsDelegate(UpdateControls));
            }
            else
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            intervalSettingsUpDownControl.Value = this.reminderSettings.ReminderTimeInMinutes;
            resetAllSettingsButton.Enabled = !this.IsDefaultSettingsCurrent();

            var nextReminderDateTime = DateTimeUtils.GetNextReminderDateTime(this.reminderSettings.ReminderTimeInMinutes);
            this.reminderInfoLabel.Text = $"Next reminder is at {nextReminderDateTime.ToString("hh:mm tt", System.Globalization.CultureInfo.CurrentCulture)}.";
            ResetTimerTimeout();
            ShowAppInNormalWindowSize();
        }



        private void ResetTimerTimeout()
        {
            reminderTriggerTimer1.Stop();
            var alarmTimeFromNow = (DateTimeUtils.GetNextReminderDateTime(this.reminderSettings.ReminderTimeInMinutes) - DateTime.Now).TotalMilliseconds;
            reminderTriggerTimer1.Interval = alarmTimeFromNow;
            reminderTriggerTimer1.Start();
        }

        private async void OnTimerTimeElapsedEventAsync(object sender, ElapsedEventArgs e)
        {
            await this.PlayReminderMusicAsync().ConfigureAwait(false);
            this.SetUIWithSettings();
        }

        private void LoadSettings()
        {
            try
            {
                reminderSettings = this.storage.Get(USER_PREFERENCES_FILENAME);
                if (!this.ValidateUserPreferences(reminderSettings))
                {
                    this.LoadDefaultSettings();
                }
            }
            catch
            {
                this.LoadDefaultSettings();
            }
        }

        private void SavePreferences()
        {
            try
            {
                if (reminderSettings != null)
                {
                    this.storage.Save(this.reminderSettings, USER_PREFERENCES_FILENAME);
                }
            }
            catch (Exception exp)
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    MessageBox.Show(exp.Message, "Error in saving preferences");
                }
            }
        }

        private bool ValidateUserPreferences(ReminderSettings userPreferences)
        {
            return !(userPreferences == null
                || string.IsNullOrWhiteSpace(userPreferences.MusicFilePath)
                || !File.Exists(userPreferences.MusicFilePath)
                || userPreferences.ReminderTimeInMinutes < AppConstants.REMINDER_INTERVAL_MINIMUM
                || userPreferences.ReminderTimeInMinutes > AppConstants.REMINDER_INTERVAL_MAXMUM);
        }

        private void LoadDefaultSettings()
        {
            if (this.defaultReminderSettings == null)
            {
                this.defaultReminderSettings = new ReminderSettings()
                {
                    MusicFilePath = MUSIC_FULL_PATH_DEFAULT,
                    ReminderTimeInMinutes = AppConstants.REMINDER_INTERVAL_MINIMUM
                };
            }

            this.reminderSettings = new ReminderSettings()
            {
                MusicFilePath = MUSIC_FULL_PATH_DEFAULT,
                ReminderTimeInMinutes = AppConstants.REMINDER_INTERVAL_MINIMUM
            };
        }

        private bool IsDefaultSettingsCurrent()
        {
            return this.defaultReminderSettings != null &&
                this.reminderSettings != null &&
                this.defaultReminderSettings.ReminderTimeInMinutes == this.reminderSettings.ReminderTimeInMinutes &&
                this.defaultReminderSettings.MusicFilePath == this.reminderSettings.MusicFilePath;
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            await this.PlayReminderMusicAsync().ConfigureAwait(false);
        }

        private async Task PlayReminderMusicAsync()
        {
            await Task.Run(() =>
            {
                using (var soundPlayer = new SoundPlayer(this.reminderSettings.MusicFilePath))
                {
                    soundPlayer.Play(); // can also use soundPlayer.PlaySync()
                }
            });
        }

        private void intervalSettingsUpDownControl_ValueChanged(object sender, EventArgs e)
        {
            this.reminderSettings.ReminderTimeInMinutes = (ushort)intervalSettingsUpDownControl.Value;
            SavePreferences();
            SetUIWithSettings();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SavePreferences();
            this.disposeResources();
        }

        private void disposeResources()
        {
            try
            {
                reminderTriggerTimer1.Stop();
                reminderTriggerTimer1.Dispose();
            }
            catch { }
        }

        private void resetDefaultSettingsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private async void playMusicButton_Click(object sender, EventArgs e)
        {
            await this.PlayReminderMusicAsync().ConfigureAwait(false);
        }

        private async void changeMusicButton_Click(object sender, EventArgs e)
        {
            musicFileOpenDialog.Filter = "Wav files|*.WAV";
            musicFileOpenDialog.InitialDirectory = this.reminderSettings.MusicFilePath;
            var musicFileOpenDialogResult = musicFileOpenDialog.ShowDialog();
            if (musicFileOpenDialogResult == DialogResult.OK)
            {
                this.reminderSettings.MusicFilePath = musicFileOpenDialog.FileName;
                this.SetUIWithSettings();
                this.SavePreferences();
                await this.PlayReminderMusicAsync().ConfigureAwait(false);
            }
        }

        private void resetAllSettingsButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                $"Reminder music will be played once on every {AppConstants.REMINDER_INTERVAL_MINIMUM} minutes.",
                "Confirm reset",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.LoadDefaultSettings();
                this.SavePreferences();
                this.SetUIWithSettings();
            }
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                systemTrayIcon.Visible = true;
                systemTrayIcon.ShowBalloonTip(3000);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                systemTrayIcon.Visible = false;
            }
        }

        private void systemTrayIcon_Click(object sender, EventArgs e)
        {
            ShowAppInNormalWindowSize();
        }

        private void ShowAppInNormalWindowSize()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            systemTrayIcon.Visible = false;
        }
    }
}
