using System;
using WorkBreakReminder.Core.Constants;

namespace WorkBreakReminder.Core.Utils
{
    /// <summary>
    /// Date time extension methods.
    /// </summary>
    public static class DateTimeUtils
    {
        /// <summary>
        /// Calculates the next reminder date time by adding the 
        /// given reminder time in minutes to the current date time.
        /// </summary>
        /// <param name="reminderTimeInMinutes">Reminder interval in minutes.</param>
        /// <returns>Date time represents the next reminder time.</returns>
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
