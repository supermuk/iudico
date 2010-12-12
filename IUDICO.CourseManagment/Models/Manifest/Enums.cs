using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public enum TimeLimitAction
    {
        [XmlEnum(SCORM.ContinueNoMessage)]
        ContinueNoMessage = 0,
        [XmlEnum(SCORM.ContinueWithMessage)]
        ContinueWithMessage = 1,
        [XmlEnum(SCORM.ExitNoMessage)]
        ExitNoMessage = 2,
        [XmlEnum(SCORM.ExitWithMessage)]
        ExitWithMessage = 3,
    }

    [Serializable]
    public enum ScormType
    {
        [XmlEnum(SCORM.SCO)]
        SCO = 0,
        [XmlEnum(SCORM.Asset)]
        Asset = 1,
    }
}