using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Determines whether the specified date time is in timeline.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="timeline">The timeline.</param>
        /// <returns>
        ///   <c>true</c> if the specified date time is in; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIn(this DateTime dateTime, Timeline timeline)
        {
            return dateTime >= timeline.StartDate && dateTime <= timeline.EndDate;
        }
    }
}