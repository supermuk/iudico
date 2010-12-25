using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public class RollupConsiderations
    {
        [XmlAttribute(SCORM.RequiredForSatisfied)]
        public Required RequiredForSatisfied; // = Required.Always;

        [XmlAttribute(SCORM.RequiredForNotSatisfied)]
        public Required RequiredForNotSatisfied; // = Required.Always;

        [XmlAttribute(SCORM.RequiredForCompleted)]
        public Required RequiredForCompleted; // = Required.Always;

        [XmlAttribute(SCORM.RequiredForIncomplete)]
        public Required RequiredForIncomplete; // = Required.Always;

        /// <summary>
        ///  This 
        /// attribute indicates if the measure should be used to determine satisfaction 
        /// during rollup when the activity is active.
        /// </summary>
        [XmlAttribute(SCORM.MeasureSatisfactionIfActive)]
        public bool MeasureSatisfactionIfActive; // = true;
    }
}