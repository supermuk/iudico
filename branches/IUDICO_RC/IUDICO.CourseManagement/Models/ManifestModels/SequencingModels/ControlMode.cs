using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public partial class ControlMode
    {
        public ControlMode()
        {
            this.Choice = true;
            this.ChoiceExit = true;
            this.Flow = false;

        }

        #region XmlAttributes

        /// <summary>
        /// Indicates that a choice sequencing 
        /// request is permitted (or not permitted if value = false) to target the children of 
        /// the activity.
        /// </summary>
        [XmlAttribute(SCORM.Choice)]
        public bool Choice { get; set; } // = true;

        /// <summary>
        /// Indicates that an active child of 
        /// this activity is permitted to terminate (or not permitted if value = false) if a 
        /// choice sequencing request is processed.
        /// </summary>
        [XmlAttribute(SCORM.ChoiceExit)]
        public bool ChoiceExit { get; set; } // = true;

        /// <summary>
        /// Indicates the flow sequencing requests is 
        /// permitted (or not permitted if value = false) to the children of this activity/
        /// </summary>
        [XmlAttribute(SCORM.Flow)] 
        public bool Flow { get; set; } // = false;

        /// <summary>
        ///  Indicates that backward targets 
        /// (in terms of activity tree traversal) are not permitted (or are permitted if value = 
        /// false) for the children of this activity.
        /// </summary>
        [XmlAttribute(SCORM.ForwardOnly)]
        public bool ForwardOnly { get; set; }

        /// <summary>
        /// Indicates 
        /// that the objective progress information for the children of the activity will only be 
        /// used (or not used if value = false) in rule evaluations and rollup if that 
        /// information was recorded during the current attempt on the activity.
        /// </summary>
        [XmlAttribute(SCORM.UseCurrentAttemptObjectiveInfo)]
        public bool UseCurrentAttemptObjectiveInfo { get; set; }

        /// <summary>
        /// Indicates 
        /// that the attempt progress information for the children of the activity will only be 
        /// used (or not used if value = false) in rule evaluations and rollup if that 
        /// information was recorded during the current attempt on the activity.
        /// </summary>
        [XmlAttribute(SCORM.UseCurrentAttemptProgressInfo)]
        public bool UseCurrentAttemptProgressInfo { get; set; }

        #endregion
    }

}