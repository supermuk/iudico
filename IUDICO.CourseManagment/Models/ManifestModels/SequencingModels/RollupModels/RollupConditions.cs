using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public class RollupConditions
    {
        /// <summary>
        ///   This attribute indicates how the rollup conditions are to be combined.  
        /// </summary>
        [XmlAttribute(SCORM.ConditionCombination)]
        public ConditionCombination ConditionCombination; // = ConditionCombination.Any;



        [XmlElement(SCORM.RollupCondition, Namespace = SCORM.ImsssNamespace)]
        public List<RollupCondition> RollupCondition;
    }
}