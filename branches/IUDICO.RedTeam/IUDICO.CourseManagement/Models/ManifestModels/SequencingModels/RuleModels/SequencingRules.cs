using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels
{
    [Serializable]
    public class SequencingRules
    {
        #region XmlElements

        [XmlElement(SCORM.PreConditionRule, Namespace = SCORM.ImsssNamespace)]
        public Rule PreConditionRule;

        [XmlElement(SCORM.ExitConditionRule, Namespace = SCORM.ImsssNamespace)]
        public Rule ExitConditionRule;

        [XmlElement(SCORM.PostConditionRule, Namespace = SCORM.ImsssNamespace)]
        public Rule PostConditionRule;

        #endregion
    }
}