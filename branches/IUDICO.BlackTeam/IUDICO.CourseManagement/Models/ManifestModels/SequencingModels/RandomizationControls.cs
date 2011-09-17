using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public partial class RandomizationControls
    {
        public RandomizationControls()
        {
            RandomizationTiming = Timing.Never;
            SelectionTiming = Timing.Never;
            ReorderChildren = false;
        }
        #region XmlAttributes

        /// <summary>
        /// This attribute indicates 
        /// when the ordering of the children of the activity should occur.
        /// </summary>
        [XmlAttribute(SCORM.RandomizationTiming)]
        public Timing RandomizationTiming { get; set; } // = Timing.Never;

        /// <summary>
        ///   This attribute indicates the number of child activities 
        /// that must be selected from the set of child activities associated with the activity .
        /// </summary>
        [XmlAttribute(SCORM.SelectCount)]
        public int SelectCount { get; set; }

        /// <summary>
        ///  This attribute indicates that 
        /// the order of the child activities is randomized.
        /// </summary>
        [XmlAttribute(SCORM.ReorderChildren)]
        public bool ReorderChildren { get; set; } // = false;

        [XmlAttribute(SCORM.SelectionTiming)]
        public Timing SelectionTiming { get; set; }// = Timing.Never;

        #endregion
    }
}