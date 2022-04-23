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
        private readonly IReminderLogic reminderLogic;
        private string musicLocation;
        private bool viewInitialized;
        private bool userForcedClose;

        public mainForm(IConfiguration appConfiguration)
        {
            InitializeComponent();
            var appSettings = appConfiguration.GetSection("appSettings").Get<AppSettings>();
            this.storage = new UserProfileFileStorage<ReminderSettings>();
            this.reminderLogic = new ReminderLogic(this, this.storage, new AppSettingsReadonly(appSettings));
            this.reminderLogic.InitializeAsync();
        }

        private delegate void UpdateControlsDelegate(IReminderSettingsReadOnly args);
        private delegate void UpdateSummaryDelegate(string args);
        private delegate void ExecuteMethod();


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

        public void OnReminder()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ExecuteMethod(showAndThenMinimizeWindow));
            }
            else
            {
                this.showAndThenMinimizeWindow();
            }
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
                    this.intervalSettingsUpDownControl.Value = reminderSettings.ReminderIntervalInMinutes;
                    this.musicLocation = reminderSettings.MusicLocation;
                    this.chkBoxPopupOnReminder.Checked = reminderSettings.PopupWindowOnEachReminder;
                    this.chkBoxClosePreference.Checked = reminderSettings.MinimizeOnCloseWindow;
                }
            }
        }

        private async void showAndThenMinimizeWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowAppInNormalWindowSize();
                await Task.Delay(1000);
                MinimizeWindow();
            }
        }

        private void MinimizeWindow()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private async Task SavePreferencesAsync()
        {
            try
            {
                await this.reminderLogic.SaveSettingsAsync(
                    new ReminderSettings(
                        this.musicLocation,
                        decimal.ToUInt16(intervalSettingsUpDownControl.Value),
                        this.chkBoxClosePreference.Checked,
                        this.chkBoxPopupOnReminder.Checked
                    ));
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

        private async void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chkBoxClosePreference.Checked && !this.userForcedClose)
            {
                MinimizeWindow();
                e.Cancel = true;
            }
            else
            {
                await this.SavePreferencesAsync();
                this.reminderLogic.Dispose();
            }
        }

        private async void playMusicButton_Click(object sender, EventArgs e)
        {
            await this.reminderLogic.PlayReminderMusicAsync();
        }

        private async void changeMusicButton_Click(object sender, EventArgs e)
        {
            // Filter only wav audio files
            musicFileOpenDialog.Filter = "Wav files|*.WAV";
            // Set the music
            musicFileOpenDialog.InitialDirectory = (await this.reminderLogic.GetCurrentSettingsAsync()).MusicLocation;
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
                showNextReminderInBaloonNotification();
                this.Hide();
            }
        }

        private void showNextReminderInBaloonNotification()
        {
            systemTrayIcon.ShowBalloonTip(500, this.Text, this.reminderInfoLabel.Text, ToolTipIcon.Info);
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
            // Let the system tray icon visible as the menu options are needed to close the window.
            // systemTrayIcon.Visible = false;
        }

        private void UpdateReminderSummary(string reminderSummary)
        {
            if (reminderSummary != null)
            {
                this.reminderInfoLabel.Text = reminderSummary;

                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.showNextReminderInBaloonNotification();
                }
            }
        }

        private async void chkBoxClosePreference_CheckedChanged(object sender, EventArgs e)
        {
            await this.SavePreferencesAsync();
        }

        private void systemTrayMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Close the app when used asked to.
            // This method will be called if any of the menu item in the
            // system tray context menu is clicked. The value for the property
            // "Tag" might be null if any new menu item is added without the tag value.
            if (e.ClickedItem.Tag == "Exit")
            {
                this.userForcedClose = true;
                this.Close();
            }

        }

    }
}
