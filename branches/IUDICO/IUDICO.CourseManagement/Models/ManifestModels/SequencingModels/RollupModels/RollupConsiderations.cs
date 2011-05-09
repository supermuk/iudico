using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public partial class RollupConsiderations
    {
        #region XmlAttributes

        [XmlAttribute(SCORM.RequiredForSatisfied)]
        public Required RequiredForSatisfied { get; set; } // = Required.Always;

        [XmlAttribute(SCORM.RequiredForNotSatisfied)]
        public Required RequiredForNotSatisfied { get; set; } // = Required.Always;

        [XmlAttribute(SCORM.RequiredForCompleted)]
        public Required RequiredForCompleted { get; set; }// = Required.Always;

        [XmlAttribute(SCORM.RequiredForIncomplete)]
        public Required RequiredForIncomplete { get; set; } // = Required.Always;

        /// <summary>
        ///  This 
        /// attribute indicates if the measure should be used to determine satisfaction 
        /// during rollup when the activity is active.
        /// </summary>
        [XmlAttribute(SCORM.MeasureSatisfactionIfActive)]
        public bool MeasureSatisfactionIfActive { get; set; } // = true;

        #endregion
    }
}