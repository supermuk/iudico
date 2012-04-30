using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public partial class LimitConditions
    {
        #region XmlAttributes

        /// <summary>
        /// This value indicates the maximum number of attempts for the activity/
        /// </summary>
        [XmlAttribute(SCORM.AttemptLimit)]
        public int AttemptLimit { get; set; } 

        /// <summary>
        /// This value indicates the maximum 
        /// time duration that the learner is permitted to spend on any single learner attempt 
        /// on the activity.  The limit applies to only the time the learner is actually 
        /// interacting with the activity and does not apply when the activity is suspended [5].  
        /// This element is used to initialize the cmi.max_time_allowed (refer to the 
        /// SCORM RTE Book [2]).  Currently, the SCO is responsible for all time tracking 
        /// and behaviors due to timing violations.
        /// </summary>
        [XmlAttribute(SCORM.AttemptAbsoluteDurationLimit)]
        public string AttemptAbsoluteDurationLimit { get; set; } 

        #endregion
    }
}