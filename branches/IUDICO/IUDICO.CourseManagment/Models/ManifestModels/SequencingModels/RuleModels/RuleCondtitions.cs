using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels
{
    [Serializable]
    public class RuleConditions
    {
        #region XmlAttributes

        [XmlAttribute(SCORM.ConditionCombination)]
        public ConditionCombination ConditionCombination;
        
        #endregion

        #region XmlElements

        [XmlElement(SCORM.RuleCondition, Namespace = SCORM.ImsssNamespace)]
        public List<RuleCondition> _RuleConditions;

        #endregion
    }
}