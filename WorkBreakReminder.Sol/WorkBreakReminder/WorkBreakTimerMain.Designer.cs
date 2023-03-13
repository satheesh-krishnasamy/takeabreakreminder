
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
            this.tabReminder = new System.Windows.Forms.TabPage();
            this.detailsGroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.reminderInfoLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gotoSettingsButton = new System.Windows.Forms.Button();
            this.btnPauseReminder = new System.Windows.Forms.Button();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.settingsGroupbox = new System.Windows.Forms.GroupBox();
            this.settingsDisplayTable = new System.Windows.Forms.TableLayoutPanel();
            this.playMusicButton = new System.Windows.Forms.Button();
            this.changeMusicButton = new System.Windows.Forms.Button();
            this.intervalSettingsUpDownControl = new System.Windows.Forms.NumericUpDown();
            this.pastMusicFilesList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.resetAllSettingsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBoxPopupOnReminder = new System.Windows.Forms.CheckBox();
            this.chkBoxClosePreference = new System.Windows.Forms.CheckBox();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.releaseNotesTextBox = new System.Windows.Forms.TextBox();
            this.musicFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.systemTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.systemTrayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitReminderApp = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabReminder.SuspendLayout();
            this.detailsGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.settingsGroupbox.SuspendLayout();
            this.settingsDisplayTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalSettingsUpDownControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.systemTrayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabReminder);
            this.tabControl.Controls.Add(this.tabOptions);
            this.tabControl.Controls.Add(this.tabAbout);
            this.tabControl.Location = new System.Drawing.Point(1, 3);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(563, 309);
            this.tabControl.TabIndex = 2;
            // 
            // tabReminder
            // 
            this.tabReminder.Controls.Add(this.detailsGroupBox);
            this.tabReminder.Location = new System.Drawing.Point(4, 24);
            this.tabReminder.Margin = new System.Windows.Forms.Padding(2);
            this.tabReminder.Name = "tabReminder";
            this.tabReminder.Padding = new System.Windows.Forms.Padding(2);
            this.tabReminder.Size = new System.Drawing.Size(555, 281);
            this.tabReminder.TabIndex = 0;
            this.tabReminder.Text = "Reminder";
            this.tabReminder.ToolTipText = "Main reminder page";
            this.tabReminder.UseVisualStyleBackColor = true;
            // 
            // detailsGroupBox
            // 
            this.detailsGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.detailsGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.detailsGroupBox.Location = new System.Drawing.Point(4, 9);
            this.detailsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.detailsGroupBox.Name = "detailsGroupBox";
            this.detailsGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.detailsGroupBox.Size = new System.Drawing.Size(547, 102);
            this.detailsGroupBox.TabIndex = 10;
            this.detailsGroupBox.TabStop = false;
            this.detailsGroupBox.Text = "Next reminder";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.reminderInfoLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 20);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(531, 24);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // reminderInfoLabel
            // 
            this.reminderInfoLabel.AutoSize = true;
            this.reminderInfoLabel.Location = new System.Drawing.Point(2, 0);
            this.reminderInfoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.reminderInfoLabel.Name = "reminderInfoLabel";
            this.reminderInfoLabel.Size = new System.Drawing.Size(224, 15);
            this.reminderInfoLabel.TabIndex = 8;
            this.reminderInfoLabel.Text = "Next reminder details will be shown here.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.gotoSettingsButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPauseReminder, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 36);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // gotoSettingsButton
            // 
            this.gotoSettingsButton.Location = new System.Drawing.Point(148, 3);
            this.gotoSettingsButton.Name = "gotoSettingsButton";
            this.gotoSettingsButton.Size = new System.Drawing.Size(133, 28);
            this.gotoSettingsButton.TabIndex = 11;
            this.gotoSettingsButton.Text = "Go to op&tions";
            this.gotoSettingsButton.UseVisualStyleBackColor = true;
            this.gotoSettingsButton.Click += new System.EventHandler(this.gotoSettingsButton_Click);
            // 
            // btnPauseReminder
            // 
            this.btnPauseReminder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPauseReminder.Location = new System.Drawing.Point(2, 2);
            this.btnPauseReminder.Margin = new System.Windows.Forms.Padding(2);
            this.btnPauseReminder.Name = "btnPauseReminder";
            this.btnPauseReminder.Size = new System.Drawing.Size(141, 28);
            this.btnPauseReminder.TabIndex = 16;
            this.btnPauseReminder.Text = "&Pause next 1 hr";
            this.btnPauseReminder.UseVisualStyleBackColor = true;
            this.btnPauseReminder.Click += new System.EventHandler(this.btnPauseReminder_Click);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.settingsGroupbox);
            this.tabOptions.Controls.Add(this.groupBox1);
            this.tabOptions.Location = new System.Drawing.Point(4, 24);
            this.tabOptions.Margin = new System.Windows.Forms.Padding(2);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(2);
            this.tabOptions.Size = new System.Drawing.Size(555, 281);
            this.tabOptions.TabIndex = 1;
            this.tabOptions.Text = "Options";
            this.tabOptions.ToolTipText = "Options page";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // settingsGroupbox
            // 
            this.settingsGroupbox.Controls.Add(this.settingsDisplayTable);
            this.settingsGroupbox.Location = new System.Drawing.Point(6, 76);
            this.settingsGroupbox.Margin = new System.Windows.Forms.Padding(2);
            this.settingsGroupbox.Name = "settingsGroupbox";
            this.settingsGroupbox.Padding = new System.Windows.Forms.Padding(2);
            this.settingsGroupbox.Size = new System.Drawing.Size(510, 198);
            this.settingsGroupbox.TabIndex = 14;
            this.settingsGroupbox.TabStop = false;
            this.settingsGroupbox.Text = "Settings";
            // 
            // settingsDisplayTable
            // 
            this.settingsDisplayTable.AutoScroll = true;
            this.settingsDisplayTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingsDisplayTable.ColumnCount = 2;
            this.settingsDisplayTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.16075F));
            this.settingsDisplayTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.83925F));
            this.settingsDisplayTable.Controls.Add(this.playMusicButton, 1, 3);
            this.settingsDisplayTable.Controls.Add(this.changeMusicButton, 1, 2);
            this.settingsDisplayTable.Controls.Add(this.intervalSettingsUpDownControl, 1, 0);
            this.settingsDisplayTable.Controls.Add(this.pastMusicFilesList, 1, 1);
            this.settingsDisplayTable.Controls.Add(this.label1, 0, 0);
            this.settingsDisplayTable.Controls.Add(this.label2, 0, 1);
            this.settingsDisplayTable.Controls.Add(this.resetAllSettingsButton, 1, 4);
            this.settingsDisplayTable.Location = new System.Drawing.Point(5, 21);
            this.settingsDisplayTable.Name = "settingsDisplayTable";
            this.settingsDisplayTable.RowCount = 5;
            this.settingsDisplayTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.5F));
            this.settingsDisplayTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.5F));
            this.settingsDisplayTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.settingsDisplayTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.settingsDisplayTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.settingsDisplayTable.Size = new System.Drawing.Size(479, 165);
            this.settingsDisplayTable.TabIndex = 0;
            // 
            // playMusicButton
            // 
            this.playMusicButton.AutoSize = true;
            this.playMusicButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playMusicButton.Location = new System.Drawing.Point(180, 87);
            this.playMusicButton.Margin = new System.Windows.Forms.Padding(2);
            this.playMusicButton.Name = "playMusicButton";
            this.playMusicButton.Size = new System.Drawing.Size(166, 25);
            this.playMusicButton.TabIndex = 3;
            this.playMusicButton.Text = "P&lay current reminder music";
            this.playMusicButton.UseVisualStyleBackColor = true;
            this.playMusicButton.Click += new System.EventHandler(this.playMusicButton_Click);
            // 
            // changeMusicButton
            // 
            this.changeMusicButton.AutoSize = true;
            this.changeMusicButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.changeMusicButton.Location = new System.Drawing.Point(180, 58);
            this.changeMusicButton.Margin = new System.Windows.Forms.Padding(2);
            this.changeMusicButton.Name = "changeMusicButton";
            this.changeMusicButton.Size = new System.Drawing.Size(133, 25);
            this.changeMusicButton.TabIndex = 4;
            this.changeMusicButton.Text = "Select other &music file";
            this.changeMusicButton.UseVisualStyleBackColor = true;
            this.changeMusicButton.Click += new System.EventHandler(this.changeMusicButton_Click);
            // 
            // intervalSettingsUpDownControl
            // 
            this.intervalSettingsUpDownControl.AutoSize = true;
            this.intervalSettingsUpDownControl.Location = new System.Drawing.Point(180, 2);
            this.intervalSettingsUpDownControl.Margin = new System.Windows.Forms.Padding(2);
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
            this.intervalSettingsUpDownControl.Size = new System.Drawing.Size(35, 23);
            this.intervalSettingsUpDownControl.TabIndex = 2;
            this.intervalSettingsUpDownControl.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.intervalSettingsUpDownControl.ValueChanged += new System.EventHandler(this.intervalSettingsUpDownControl_ValueChanged);
            // 
            // pastMusicFilesList
            // 
            this.pastMusicFilesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pastMusicFilesList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pastMusicFilesList.FormattingEnabled = true;
            this.pastMusicFilesList.Location = new System.Drawing.Point(181, 30);
            this.pastMusicFilesList.Name = "pastMusicFilesList";
            this.pastMusicFilesList.Size = new System.Drawing.Size(295, 23);
            this.pastMusicFilesList.TabIndex = 17;
            this.pastMusicFilesList.SelectionChangeCommitted += new System.EventHandler(this.pastMusicFilesList_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reminder interval in minutes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reminder music:";
            // 
            // resetAllSettingsButton
            // 
            this.resetAllSettingsButton.AutoSize = true;
            this.resetAllSettingsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resetAllSettingsButton.BackColor = System.Drawing.Color.LightCoral;
            this.resetAllSettingsButton.Location = new System.Drawing.Point(180, 117);
            this.resetAllSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetAllSettingsButton.Name = "resetAllSettingsButton";
            this.resetAllSettingsButton.Size = new System.Drawing.Size(158, 25);
            this.resetAllSettingsButton.TabIndex = 14;
            this.resetAllSettingsButton.Text = "&Reset all to default settings";
            this.resetAllSettingsButton.UseVisualStyleBackColor = false;
            this.resetAllSettingsButton.Click += new System.EventHandler(this.resetAllSettingsButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBoxPopupOnReminder);
            this.groupBox1.Controls.Add(this.chkBoxClosePreference);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(512, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional options";
            // 
            // chkBoxPopupOnReminder
            // 
            this.chkBoxPopupOnReminder.AutoSize = true;
            this.chkBoxPopupOnReminder.Location = new System.Drawing.Point(4, 39);
            this.chkBoxPopupOnReminder.Margin = new System.Windows.Forms.Padding(2);
            this.chkBoxPopupOnReminder.Name = "chkBoxPopupOnReminder";
            this.chkBoxPopupOnReminder.Size = new System.Drawing.Size(202, 19);
            this.chkBoxPopupOnReminder.TabIndex = 1;
            this.chkBoxPopupOnReminder.Text = "P&opup window on each reminder";
            this.chkBoxPopupOnReminder.UseVisualStyleBackColor = true;
            this.chkBoxPopupOnReminder.CheckedChanged += new System.EventHandler(this.chkBoxPopupOnReminder_CheckedChanged);
            // 
            // chkBoxClosePreference
            // 
            this.chkBoxClosePreference.AutoSize = true;
            this.chkBoxClosePreference.Location = new System.Drawing.Point(4, 18);
            this.chkBoxClosePreference.Margin = new System.Windows.Forms.Padding(2);
            this.chkBoxClosePreference.Name = "chkBoxClosePreference";
            this.chkBoxClosePreference.Size = new System.Drawing.Size(181, 19);
            this.chkBoxClosePreference.TabIndex = 0;
            this.chkBoxClosePreference.Text = "&Minimize window upon close";
            this.chkBoxClosePreference.UseVisualStyleBackColor = true;
            this.chkBoxClosePreference.CheckedChanged += new System.EventHandler(this.chkBoxClosePreference_CheckedChanged);
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.releaseNotesTextBox);
            this.tabAbout.Location = new System.Drawing.Point(4, 24);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabAbout.Size = new System.Drawing.Size(555, 281);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // releaseNotesTextBox
            // 
            this.releaseNotesTextBox.Location = new System.Drawing.Point(6, 4);
            this.releaseNotesTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.releaseNotesTextBox.Multiline = true;
            this.releaseNotesTextBox.Name = "releaseNotesTextBox";
            this.releaseNotesTextBox.ReadOnly = true;
            this.releaseNotesTextBox.Size = new System.Drawing.Size(356, 247);
            this.releaseNotesTextBox.TabIndex = 0;
            // 
            // systemTrayIcon
            // 
            this.systemTrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.systemTrayIcon.BalloonTipText = "Show take a break app.";
            this.systemTrayIcon.BalloonTipTitle = "Take a break reminder";
            this.systemTrayIcon.ContextMenuStrip = this.systemTrayMenuStrip;
            this.systemTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTrayIcon.Icon")));
            this.systemTrayIcon.Text = "Take a break reminder";
            this.systemTrayIcon.Visible = true;
            this.systemTrayIcon.Click += new System.EventHandler(this.systemTrayIcon_Click);
            // 
            // systemTrayMenuStrip
            // 
            this.systemTrayMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.systemTrayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitReminderApp});
            this.systemTrayMenuStrip.Name = "systemTrayMenuStrip";
            this.systemTrayMenuStrip.Size = new System.Drawing.Size(104, 48);
            this.systemTrayMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.systemTrayMenuStrip_ItemClicked);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "&Show";
            // 
            // exitReminderApp
            // 
            this.exitReminderApp.Name = "exitReminderApp";
            this.exitReminderApp.Size = new System.Drawing.Size(103, 22);
            this.exitReminderApp.Tag = "Exit";
            this.exitReminderApp.Text = "E&xit";
            this.exitReminderApp.ToolTipText = "Exit the reminder app";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(575, 319);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "mainForm";
            this.Text = "Take a break";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabReminder.ResumeLayout(false);
            this.detailsGroupBox.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.settingsGroupbox.ResumeLayout(false);
            this.settingsDisplayTable.ResumeLayout(false);
            this.settingsDisplayTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalSettingsUpDownControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            this.systemTrayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabReminder;
        private System.Windows.Forms.OpenFileDialog musicFileOpenDialog;
        private System.Windows.Forms.GroupBox detailsGroupBox;
        private System.Windows.Forms.Label reminderInfoLabel;
        private System.Windows.Forms.NotifyIcon systemTrayIcon;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkBoxClosePreference;
        private System.Windows.Forms.CheckBox chkBoxPopupOnReminder;
        private System.Windows.Forms.ContextMenuStrip systemTrayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitReminderApp;
        private System.Windows.Forms.Button btnPauseReminder;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.GroupBox settingsGroupbox;
        private System.Windows.Forms.ComboBox pastMusicFilesList;
        private System.Windows.Forms.Button resetAllSettingsButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeMusicButton;
        private System.Windows.Forms.Button playMusicButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown intervalSettingsUpDownControl;
        private System.Windows.Forms.Button gotoSettingsButton;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.TextBox releaseNotesTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel settingsDisplayTable;
    }
}

