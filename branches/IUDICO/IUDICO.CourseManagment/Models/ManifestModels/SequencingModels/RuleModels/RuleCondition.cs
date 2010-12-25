using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels
{
    [Serializable]
    public class RuleCondition
    {
        [XmlAttribute(SCORM.ReferencedObjective)]
        public string ReferencedObjective;

        /// <summary>
        /// The value used as a threshold during measure-based condition evaluations.
        /// </summary>
        [XmlAttribute(SCORM.MeasureThreshold)]
        public float MeasureThreshold;

        [XmlAttribute(SCORM.Operator)]
        public Operator Operator; // = Operator.NoOp;

        [XmlAttribute(SCORM.Condition)]
        public Condition Condition; 
    }
}
