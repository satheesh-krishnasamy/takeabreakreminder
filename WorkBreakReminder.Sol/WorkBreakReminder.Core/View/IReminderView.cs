using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.View
{
    /// <summary>
    /// Interface represents the reminder view's responsibilities.
    /// </summary>
    public interface IReminderView
    {
        /// <summary>
        /// Method triggered when the reminder settings are to be shown in view due to any updates happened.
        /// </summary>
        /// <param name="reminderSettings">Reminder settings to be shown.</param>
        void UpdateUIWithReminderSettings(IReminderSettingsReadOnly reminderSettings);

        /// <summary>
        /// Method called when the reminder time reached.
        /// </summary>
        void OnReminder(NotificationEventArgs args);

        /// <summary>
        /// Method called just before calling the update UI method.
        /// </summary>
        void BeforeInitViewCallback();

        /// <summary>
        /// Method called just after calling the update UI method.
        /// </summary>
        void AfterInitViewCallback();

        /// <summary>
        /// Method called with the reminder details as summary.
        /// </summary>
        /// <param name="summary">Reminder details to be shown in UI.</param>
        void SetNextReminderTime(string summary);
    }
}
