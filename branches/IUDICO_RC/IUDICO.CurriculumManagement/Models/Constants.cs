using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    public static class Constants
    {
       public const int DefaultThresholdOfSuccess = 50;
        public static readonly DateTime MinAllowedDateTime = new DateTime(1900, 1, 1);
        public static readonly DateTime MaxAllowedDateTime = new DateTime(2200, 1, 1);
        public const int MaxStringFieldLength = 50;
        public const int NoCourseId = -1;
    }
}