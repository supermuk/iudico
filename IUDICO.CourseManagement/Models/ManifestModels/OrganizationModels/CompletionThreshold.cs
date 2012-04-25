using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class CompletionThreshold
    {
        #region XmlAttributes

        [XmlAttribute(SCORM.CompletedByMeasure)]
        public bool CompletedByMeasure;

        [XmlAttribute(SCORM.MinProgressMeasure)]
        public float MinProgressMeasure;

        [XmlAttribute(SCORM.ProgressWeight)]
        public float ProgressWeight;

        #endregion
    }
}
