using System;
using WorkBreakReminder.Core.Constants;

namespace WorkBreakReminder.Core.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetNextReminderDateTime(ushort reminderTimeInMinutes)
        {
            DateTime reminderDateTime;
            if (reminderTimeInMinutes > 0)
            {
                reminderDateTime = DateTime.Now.AddMinutes(reminderTimeInMinutes);
                var extraMinutes = reminderDateTime.Minute % reminderTimeInMinutes;
                if (extraMinutes > 0)
                {
                    reminderDateTime = reminderDateTime.AddMinutes(-(extraMinutes));
                }
            }
            else
            {
                reminderDateTime = DateTime.Now.AddMinutes(AppConstants.REMINDER_INTERVAL_DEFAULT);
            }

            return reminderDateTime
                .AddMilliseconds(-reminderDateTime.Millisecond)
                .AddSeconds(-reminderDateTime.Second);
        }
    }
}
