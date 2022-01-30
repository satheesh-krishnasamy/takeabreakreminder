using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core.View
{
    public interface IReminderView
    {
        void UpdateUIWithReminderSettings(IReminderSettingsReadOnly reminderSettings);

        //void NotifyMessage(string message, NotificationLevel level);

        void BeforeInitViewCallback();
        void AfterInitViewCallback();

        void SetNextReminderTime(string summary);
    }
}
