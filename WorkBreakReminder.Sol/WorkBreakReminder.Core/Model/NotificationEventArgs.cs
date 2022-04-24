using System;
using System.Collections.Generic;
using System.Text;

namespace WorkBreakReminder.Core.Model
{
    public sealed class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(UserNotificationPrefernces prefernces)
        {
            this.NotificationPrefernces = prefernces;
        }

        public UserNotificationPrefernces NotificationPrefernces { get; private set; }
    }

    [Flags]
    public enum UserNotificationPrefernces
    {
        None = 1,
        FocusWindow = 2,
        ShowAlerts = 4
    }
}
