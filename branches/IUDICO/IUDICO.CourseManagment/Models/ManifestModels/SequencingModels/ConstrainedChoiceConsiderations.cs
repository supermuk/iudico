using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public class ConstrainedChoiceConsiderations
    {
        #region XmlAttributes

        /// <summary>
        ///   This attribute indicates 
        /// that attempts on children activities should not begin unless the current activity is 
        /// the parent.
        /// </summary>
        [XmlAttribute(SCORM.PreventActivation)]
        public bool PreventActivation; // = false;

        /// <summary>
        /// This attribute indicates that 
        /// only activities which are logically “next” from the constrained activities can be 
        /// targets of a choice navigation request. 
        /// </summary>
        [XmlAttribute(SCORM.ConstrainChoice)]
        public bool ConstrainChoice; // = false;

        #endregion 
    }
}