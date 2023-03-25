using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public partial class mainForm : Form, IReminderView, IDisposable
    {
        private const string ReminderAppPaused = "Paused";
        private const string ReminderAppCanBePaused = "CanBePaused";
        private const string titleTextPrefix = "Take a break reminder";
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

            SystemEvents.PowerModeChanged += SystemEvents_OnPowerModeChanged;

            this.Text = titleTextPrefix + " - " + this.ProductVersion;
        }

        private delegate void UpdateControlsDelegate(IReminderSettingsReadOnly args);
        private delegate void UpdateSummaryDelegate(string args);
        private delegate void ExecuteMethod(NotificationEventArgs args);


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

        public void OnReminder(NotificationEventArgs args)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ExecuteMethod(showAndThenMinimizeWindow), args);
            }
            else
            {
                this.showAndThenMinimizeWindow(args);
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
                    BindRecentMusicFilesDropdownList(reminderSettings.RecentReminderFiles, reminderSettings.MusicLocation);
                }
            }
        }

        private async void showAndThenMinimizeWindow(NotificationEventArgs args)
        {
            if (args.NotificationPrefernces == UserNotificationPrefernces.FocusWindow)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    ShowAppInNormalWindowSize();
                    await Task.Delay(1000);
                    MinimizeWindow();
                }
            }

            SetReminderCanBePaused();
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

                var reminderSettings = await this.reminderLogic.GetCurrentSettingsAsync();
                BindRecentMusicFilesDropdownList(reminderSettings.RecentReminderFiles, reminderSettings.MusicLocation);

            }
            catch (Exception exp)
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    MessageBox.Show(exp.Message, "Error in saving preferences");
                }
            }
        }

        private void BindRecentMusicFilesDropdownList(List<RecentFile> recentFiles, string currentMusicFile)
        {
            if (recentFiles != null && recentFiles.Count > 0)
            {

                if (pastMusicFilesList.Items != null && !string.IsNullOrWhiteSpace(currentMusicFile))
                {
                    foreach (RecentFile ddlItem in pastMusicFilesList.Items)
                    {
                        if (currentMusicFile.Equals(ddlItem.FilePath))
                        {
                            return;
                        }
                    }
                }


                var items = recentFiles.OrderByDescending(p => p.AccessedOn);
                pastMusicFilesList.DisplayMember = "FileName";
                pastMusicFilesList.ValueMember = "FilePath";
                var bindingSource = new BindingSource(items, null);

                if (!string.IsNullOrWhiteSpace(currentMusicFile))
                {
                    int position = -1;
                    foreach (var recentFile in items)
                    {
                        position++;
                        if (recentFile?.FilePath != null && recentFile.FilePath.Equals(currentMusicFile))
                        {
                            break;
                        }
                    }

                    if (position < items.Count())
                    {
                        bindingSource.Position = position;
                    }
                }
                bindingSource.RaiseListChangedEvents = false;
                pastMusicFilesList.DataSource = bindingSource;
                bindingSource.RaiseListChangedEvents = true;

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
            systemTrayIcon.BalloonTipTitle = titleTextPrefix;
            systemTrayIcon.ShowBalloonTip(500, titleTextPrefix, this.reminderInfoLabel.Text, ToolTipIcon.Info);
        }

        private void systemTrayIcon_Click(object sender, EventArgs e)
        {
            var mouseClickEvent = e as MouseEventArgs;
            if (e == null || mouseClickEvent == null || mouseClickEvent.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    ShowAppInNormalWindowSize();
                }
                else
                {
                    this.MinimizeWindow();
                }
            }
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
            else if (e.ClickedItem.Tag == "Show")
            {
                ShowAppInNormalWindowSize();
            }

        }

        private async void chkBoxPopupOnReminder_CheckedChanged(object sender, EventArgs e)
        {
            await this.SavePreferencesAsync();
        }

        private async void btnPauseReminder_Click(object sender, EventArgs e)
        {
            if (btnPauseReminder.Tag == ReminderAppPaused)
            {
                if (await this.reminderLogic.CancelDNDAsync())
                {
                    SetReminderCanBePaused();
                }
            }
            else
            {
                if (await this.reminderLogic.DoNotDisturbForAnHourAsync())
                {
                    UpdateUIAsPaused();
                }
            }

        }

        private void SetReminderCanBePaused()
        {
            btnPauseReminder.Text = "Pause 1 hour";
            btnPauseReminder.Tag = ReminderAppCanBePaused;
        }

        private void UpdateUIAsPaused()
        {
            btnPauseReminder.Text = "Resume";
            btnPauseReminder.Tag = ReminderAppPaused;
        }

        private void SystemEvents_OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
                this.reminderLogic.ResetReminder();
        }

        private async void pastMusicFilesList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (pastMusicFilesList.SelectedValue == null)
                return;

            var selectedFile = pastMusicFilesList.SelectedValue.ToString();
            var currentSettings = await this.reminderLogic.GetCurrentSettingsAsync();
            if (selectedFile != currentSettings.MusicLocation)
            {
                this.musicLocation = selectedFile;
                await this.SavePreferencesAsync();
            }
        }

        private void gotoSettingsButton_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("tabOptions");
        }


        public void Dispose()
        {
            this.DisposeInternal(true);
        }

        private void DisposeInternal(bool isCalledExplicitly)
        {
            if (isCalledExplicitly)
                GC.SuppressFinalize(this);

            try
            {
                base.Dispose();
                SystemEvents.PowerModeChanged -= SystemEvents_OnPowerModeChanged;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + " ST: " + ex.StackTrace); }
        }

        ~mainForm()
        {
            this.DisposeInternal(false);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.releaseNotesTextBox.Text = File.ReadAllText("releasenotes.txt");
            }
            catch (Exception exp)
            {
                this.releaseNotesTextBox.Text = exp.Message;
            }
            this.releaseNotesTextBox.ReadOnly = true;
        }
    }
}
