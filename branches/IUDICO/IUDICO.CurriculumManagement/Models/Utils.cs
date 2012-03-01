using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models
{
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Determines whether the specified date time is between start and end.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>
        ///   <c>true</c> if the specified date time is in; otherwise, <c>false</c>.
        /// </returns>
        public static bool Between(this DateTime dateTime, DateTime? start, DateTime? end)
        {
            return start.HasValue && end.HasValue ?
                dateTime >= start.Value && dateTime <= end.Value :
                false;
        }
    }
}