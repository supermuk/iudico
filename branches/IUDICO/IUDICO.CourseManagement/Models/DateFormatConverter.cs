using System;
using System.Collections.Generic;
using System.Globalization;

namespace IUDICO.CourseManagement.Models
{
    public static class DateFormatConverter
    {
        public static string DataConvert(DateTime date)
        {
            string dataformat;
            DateTime now = DateTime.Now;
            if (date.Year != now.Year)
            {
                dataformat = "{0:MM/dd/yy}";
            }
            else if (date.Month != now.Month || date.Day != now.Day)
            {
                dataformat = "{0:MMM d}";
                return string.Format(dataformat, date);
            }
            else
            {
                dataformat = "{0:t}";
            }
            return string.Format(new CultureInfo("en-US"), dataformat, date);
        }
    }
}
