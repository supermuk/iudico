using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels
{
    [Serializable]
    public class RuleConditions
    {
        [XmlAttribute(SCORM.ConditionCombination)]
        public ConditionCombination ConditionCombination;

        [XmlElement(SCORM.RuleCondition, Namespace = SCORM.ImsssNamespace)]
        public List<RuleCondition> _RuleConditions;
    }
}