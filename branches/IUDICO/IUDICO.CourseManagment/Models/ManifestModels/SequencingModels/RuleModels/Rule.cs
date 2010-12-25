using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels
{
    [Serializable]
    public class Rule
    {
        [XmlElement(SCORM.RuleConditions, Namespace = SCORM.ImsssNamespace)]
        public RuleConditions RuleConditions;

        [XmlElement(SCORM.RuleAction, Namespace = SCORM.ImsssNamespace)]
        public RuleAction RuleAction;
    }
}