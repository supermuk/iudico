using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public class DeliveryControls
    {
        #region XmlAttributes

        /// <summary>
        ///  This attribute indicates that the 
        /// objective progress information and activity/attempt progress information for the 
        /// attempt should be recorded (true or false) and the data will contribute to the rollup 
        /// for its parent activity.
        /// </summary>
        [XmlAttribute(SCORM.Tracked)]
        public bool Tracked; // = true;

        /// <summary>
        /// This attribute 
        /// indicates that the attempt completion status for the activity will be set by the SCO 
        /// (true or false).
        /// </summary>
        [XmlAttribute(SCORM.CompletionSetByContent)]
        public bool CompletionSetByContent; // = false;

        /// <summary>
        ///  This attribute 
        /// indicates that the objective satisfied status for the activity’s associated objective 
        /// that contributes to rollup will be set by the SCO.
        /// </summary>
        [XmlAttribute(SCORM.ObjectiveSetByContent)]
        public bool ObjectiveSetByContent; // = false

        #endregion
    }
}