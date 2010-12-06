using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseMgt.Models.Manifest
{
    public enum TimeLimitAction
    {
        ContinueNoMessage = 0,
        ContinueWithMessage = 1,
        ExitNoMessage = 2,
        ExitWithMessage = 3,
    }
}