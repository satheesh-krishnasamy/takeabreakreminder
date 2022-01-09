
namespace WorkBreakReminder
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.resetAllSettingsButton = new System.Windows.Forms.Button();
            this.settingsGroupbox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.changeMusicButton = new System.Windows.Forms.Button();
            this.playMusicButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.intervalSettingsUpDownControl = new System.Windows.Forms.NumericUpDown();
            this.detailsGroupBox = new System.Windows.Forms.GroupBox();
            this.reminderInfoLabel = new System.Windows.Forms.Label();
            this.musicFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.systemTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.settingsGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalSettingsUpDownControl)).BeginInit();
            this.detailsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Location = new System.Drawing.Point(1, 5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(524, 342);
            this.tabControl.TabIndex = 2;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.resetAllSettingsButton);
            this.tabSettings.Controls.Add(this.settingsGroupbox);
            this.tabSettings.Controls.Add(this.detailsGroupBox);
            this.tabSettings.Location = new System.Drawing.Point(4, 37);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(516, 301);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "Reminder";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // resetAllSettingsButton
            // 
            this.resetAllSettingsButton.AutoSize = true;
            this.resetAllSettingsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetAllSettingsButton.Location = new System.Drawing.Point(191, 240);
            this.resetAllSettingsButton.Name = "resetAllSettingsButton";
            this.resetAllSettingsButton.Size = new System.Drawing.Size(256, 38);
            this.resetAllSettingsButton.TabIndex = 14;
            this.resetAllSettingsButton.Text = "&Reset all to default settings";
            this.resetAllSettingsButton.UseVisualStyleBackColor = true;
            this.resetAllSettingsButton.Click += new System.EventHandler(this.resetAllSettingsButton_Click);
            // 
            // settingsGroupbox
            // 
            this.settingsGroupbox.Controls.Add(this.label3);
            this.settingsGroupbox.Controls.Add(this.label2);
            this.settingsGroupbox.Controls.Add(this.changeMusicButton);
            this.settingsGroupbox.Controls.Add(this.playMusicButton);
            this.settingsGroupbox.Controls.Add(this.label1);
            this.settingsGroupbox.Controls.Add(this.intervalSettingsUpDownControl);
            this.settingsGroupbox.Location = new System.Drawing.Point(13, 121);
            this.settingsGroupbox.Name = "settingsGroupbox";
            this.settingsGroupbox.Size = new System.Drawing.Size(501, 167);
            this.settingsGroupbox.TabIndex = 13;
            this.settingsGroupbox.TabStop = false;
            this.settingsGroupbox.Text = "Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "minutes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reminder music:";
            // 
            // changeMusicButton
            // 
            this.changeMusicButton.AutoSize = true;
            this.changeMusicButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.changeMusicButton.Location = new System.Drawing.Point(242, 74);
            this.changeMusicButton.Name = "changeMusicButton";
            this.changeMusicButton.Size = new System.Drawing.Size(88, 38);
            this.changeMusicButton.TabIndex = 4;
            this.changeMusicButton.Text = "&Change";
            this.changeMusicButton.UseVisualStyleBackColor = true;
            this.changeMusicButton.Click += new System.EventHandler(this.changeMusicButton_Click);
            // 
            // playMusicButton
            // 
            this.playMusicButton.AutoSize = true;
            this.playMusicButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playMusicButton.Location = new System.Drawing.Point(178, 74);
            this.playMusicButton.Name = "playMusicButton";
            this.playMusicButton.Size = new System.Drawing.Size(58, 38);
            this.playMusicButton.TabIndex = 3;
            this.playMusicButton.Text = "&Play";
            this.playMusicButton.UseVisualStyleBackColor = true;
            this.playMusicButton.Click += new System.EventHandler(this.playMusicButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reminder interval:";
            // 
            // intervalSettingsUpDownControl
            // 
            this.intervalSettingsUpDownControl.AutoSize = true;
            this.intervalSettingsUpDownControl.Location = new System.Drawing.Point(178, 34);
            this.intervalSettingsUpDownControl.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.intervalSettingsUpDownControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.intervalSettingsUpDownControl.Name = "intervalSettingsUpDownControl";
            this.intervalSettingsUpDownControl.Size = new System.Drawing.Size(54, 34);
            this.intervalSettingsUpDownControl.TabIndex = 2;
            this.intervalSettingsUpDownControl.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.intervalSettingsUpDownControl.ValueChanged += new System.EventHandler(this.intervalSettingsUpDownControl_ValueChanged);
            // 
            // detailsGroupBox
            // 
            this.detailsGroupBox.Controls.Add(this.reminderInfoLabel);
            this.detailsGroupBox.Location = new System.Drawing.Point(7, 16);
            this.detailsGroupBox.Name = "detailsGroupBox";
            this.detailsGroupBox.Size = new System.Drawing.Size(507, 99);
            this.detailsGroupBox.TabIndex = 10;
            this.detailsGroupBox.TabStop = false;
            this.detailsGroupBox.Text = "Next reminder";
            // 
            // reminderInfoLabel
            // 
            this.reminderInfoLabel.AutoSize = true;
            this.reminderInfoLabel.Location = new System.Drawing.Point(6, 30);
            this.reminderInfoLabel.Name = "reminderInfoLabel";
            this.reminderInfoLabel.Size = new System.Drawing.Size(368, 28);
            this.reminderInfoLabel.TabIndex = 8;
            this.reminderInfoLabel.Text = "Next reminder details will be shown here.";
            // 
            // systemTrayIcon
            // 
            this.systemTrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.systemTrayIcon.BalloonTipText = "Show take a break app.";
            this.systemTrayIcon.BalloonTipTitle = "Take a break reminder";
            this.systemTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTrayIcon.Icon")));
            this.systemTrayIcon.Text = "Take a break reminder";
            this.systemTrayIcon.Visible = true;
            this.systemTrayIcon.Click += new System.EventHandler(this.systemTrayIcon_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(529, 350);
            this.Controls.Add(this.tabControl);
            this.Name = "mainForm";
            this.Text = "Take a break";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.settingsGroupbox.ResumeLayout(false);
            this.settingsGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalSettingsUpDownControl)).EndInit();
            this.detailsGroupBox.ResumeLayout(false);
            this.detailsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.OpenFileDialog musicFileOpenDialog;
        private System.Windows.Forms.GroupBox detailsGroupBox;
        private System.Windows.Forms.Label reminderInfoLabel;
        private System.Windows.Forms.GroupBox settingsGroupbox;
        private System.Windows.Forms.NumericUpDown intervalSettingsUpDownControl;
        private System.Windows.Forms.Button resetAllSettingsButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeMusicButton;
        private System.Windows.Forms.Button playMusicButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon systemTrayIcon;
    }
}

