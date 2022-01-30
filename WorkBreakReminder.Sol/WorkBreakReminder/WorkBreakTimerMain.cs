using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkBreakReminder.Config;
using WorkBreakReminder.Core;
using WorkBreakReminder.Core.Constants;
using WorkBreakReminder.Core.Logic;
using WorkBreakReminder.Core.Model;
using WorkBreakReminder.Core.Storage.Extensions;
using WorkBreakReminder.Core.View;

namespace WorkBreakReminder
{
    public partial class mainForm : Form, IReminderView
    {
        private readonly IReminderStorage<ReminderSettings, string> storage;
        private readonly ReminderLogic reminderLogic;
        private string musicLocation;
        private bool viewInitialized;

        public mainForm(IConfiguration appConfiguration)
        {
            InitializeComponent();
            var appSettings = appConfiguration.GetSection("appSettings").Get<AppSettings>();
            this.storage = new UserProfileFileStorage<ReminderSettings>();
            this.reminderLogic = new ReminderLogic(this, this.storage, new AppSettingsReadonly(appSettings));
            this.reminderLogic.Initialize();
        }

        private delegate void UpdateControlsDelegate(IReminderSettingsReadOnly args);
        private delegate void UpdateSummaryDelegate(string args);

        private void SetUIWithSettings(IReminderSettingsReadOnly reminderSettings)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateControlsDelegate(UpdateControls), reminderSettings);
            }
            else
            {
                UpdateControls(reminderSettings);
            }
        }

        private void UpdateControls(IReminderSettingsReadOnly args)
        {
            //if (args != null && args.Length > 0)
            {
                var reminderSettings = args;// args[0] as IReminderSettingsReadOnly;
                if (reminderSettings != null)
                {
                    intervalSettingsUpDownControl.Value = reminderSettings.ReminderIntervalInMinutes;
                    this.musicLocation = reminderSettings.MusicLocation;
                }
            }
        }

        private async void DoMinimizeWindowAsync()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowAppInNormalWindowSize();
                await Task.Delay(1000);
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private async Task SavePreferencesAsync()
        {
            try
            {
                await this.reminderLogic.SaveSettingsAsync(
                    new ReminderSettings(this.musicLocation, decimal.ToUInt16(intervalSettingsUpDownControl.Value)))
                    ;
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
                || string.IsNullOrWhiteSpace(userPreferences.MusicLocation)
                || !File.Exists(userPreferences.MusicLocation)
                || userPreferences.ReminderIntervalInMinutes < AppConstants.REMINDER_INTERVAL_MINIMUM
                || userPreferences.ReminderIntervalInMinutes > AppConstants.REMINDER_INTERVAL_MAXMUM);
        }

        private async void intervalSettingsUpDownControl_ValueChanged(object sender, EventArgs e)
        {
            if (this.viewInitialized)
            {
                await this.SavePreferencesAsync();
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reminderLogic.Dispose();
        }

        private async void playMusicButton_Click(object sender, EventArgs e)
        {
            await this.reminderLogic.PlayReminderMusicAsync();
        }

        private async void changeMusicButton_Click(object sender, EventArgs e)
        {
            musicFileOpenDialog.Filter = "Wav files|*.WAV";
            musicFileOpenDialog.InitialDirectory = this.reminderLogic.LoadSettings().MusicLocation;
            var musicFileOpenDialogResult = musicFileOpenDialog.ShowDialog();
            if (musicFileOpenDialogResult == DialogResult.OK)
            {
                this.musicLocation = musicFileOpenDialog.FileName;
                await this.SavePreferencesAsync();
            }
        }

        private async void resetAllSettingsButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                $"Reminder music will be played once on every {AppConstants.REMINDER_INTERVAL_DEFAULT} minutes.",
                "Confirm reset",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                await this.reminderLogic.ResetToDefaultSettingsAsync();
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

        public void UpdateUIWithReminderSettings(IReminderSettingsReadOnly reminderSettings)
        {
            this.SetUIWithSettings(reminderSettings);
        }

        public void BeforeInitViewCallback()
        {
            this.viewInitialized = false;
        }

        public void AfterInitViewCallback()
        {
            this.viewInitialized = true;
        }

        public void SetNextReminderTime(string summary)
        {
            if (summary != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new UpdateSummaryDelegate(UpdateReminderSummary), summary);
                }
                else
                {
                    UpdateReminderSummary(summary);
                }
            }
        }

        private void UpdateReminderSummary(string reminderSummary)
        {
            if (reminderSummary != null)
            {
                this.reminderInfoLabel.Text = reminderSummary;
            }
        }
    }
}
