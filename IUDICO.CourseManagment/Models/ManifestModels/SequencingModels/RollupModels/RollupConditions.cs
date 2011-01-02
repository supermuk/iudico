using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public class RollupConditions
    {
        #region XmlAttributes

        /// <summary>
        ///   This attribute indicates how the rollup conditions are to be combined.  
        /// </summary>
        [XmlAttribute(SCORM.ConditionCombination)]
        public ConditionCombination ConditionCombination; // = ConditionCombination.Any;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.RollupCondition, Namespace = SCORM.ImsssNamespace)]
        public List<RollupCondition> RollupCondition;

        #endregion
    }
}