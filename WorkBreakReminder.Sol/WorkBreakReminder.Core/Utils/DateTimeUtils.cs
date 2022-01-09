﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkBreakReminder.Core.Constants;

namespace WorkBreakReminder.Core.Utils
{
    public class DateTimeUtils
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
                reminderDateTime = DateTime.Now.AddMinutes(AppConstants.REMINDER_INTERVAL_MINIMUM);
            }

            return reminderDateTime
                .AddMilliseconds(-reminderDateTime.Millisecond)
                .AddSeconds(-reminderDateTime.Second);
        }
    }
}