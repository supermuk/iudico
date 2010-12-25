using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public class RandomizationControls
    {
        /// <summary>
        /// This attribute indicates 
        /// when the ordering of the children of the activity should occur.
        /// </summary>
        [XmlAttribute(SCORM.RandomizationTiming)]
        public Timing RandomizationTiming; // = Timing.Never;

        /// <summary>
        ///   This attribute indicates the number of child activities 
        /// that must be selected from the set of child activities associated with the activity .
        /// </summary>
        [XmlAttribute(SCORM.SelectCount)]
        public int SelectCount;

        /// <summary>
        ///  This attribute indicates that 
        /// the order of the child activities is randomized.
        /// </summary>
        [XmlAttribute(SCORM.ReorderChildren)]
        public bool ReorderChildren; // = false;

        [XmlAttribute(SCORM.SelectionTiming)]
        public Timing SelectionTiming; // = Timing.Never;
    }
}