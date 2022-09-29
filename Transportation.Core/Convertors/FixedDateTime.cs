using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Transportation.Core.Conventors
{
    public static class FixedDateTime
    {
        public static string PersianDateTime(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            string hourTime = pc.GetHour(value).ToString("00")
                + ":" + pc.GetMinute(value).ToString("00") + ":" + pc.GetSecond(value).ToString("00");

            string dateTime =hourTime + " - " + pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00")
                + "/" + pc.GetDayOfMonth(value).ToString("00");
            return dateTime;
        }
    }
}
