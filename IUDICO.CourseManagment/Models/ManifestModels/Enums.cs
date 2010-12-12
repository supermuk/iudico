using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels
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