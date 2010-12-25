using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public class RollupCondition
    {
        /// <summary>
        /// The unary logical operator to be applied to the individual condition
        /// </summary>
        [XmlAttribute(SCORM.RollupCondition)]
        public Operator Operator; // = Operator.NoOp;

        /// <summary>
        ///  Indicates the condition element for the rule. 
        /// </summary>
        [XmlAttribute(SCORM.Condition)]
        public Condition Condition;
    }
}