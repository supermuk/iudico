using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels
{
    [Serializable]
    public class MapInfo
    {
        #region XmlAttributes

        /// <summary>
        /// The identifier of the global shared objective 
        /// targeted for the mapping [5].  The underlying data type for the 
        /// targetObjectiveID, is a unique identifier.  Since an empty characterstring does 
        /// not provide sufficient semantic information to uniquely identify which global 
        /// shared objective is being targeted, then the targetObjectiveID attribute cannot 
        /// be an empty characterstring and cannot contain all whitespace characters (which 
        /// could be transcribed as an empty characterstring by an XML parser).
        /// </summary>
        [XmlAttribute(SCORM.TargetObjectiveId)]
        public string TargetObjectiveId;

        /// <summary>
        /// This attribute indicates that the 
        /// raw score for the identified local objective should be retrieved (true or false) 
        /// from the identified shared global objective during launch of the activity.
        /// </summary>
        [XmlAttribute(SCORM.ReadRawScore)]
        public bool ReadRawScore; // = true

        /// <summary>
        ///  This attribute indicates that the 
        /// minimum value in the range of the raw score for the identified local objective 
        /// should be retrieved (true or false) from the identified shared global objective 
        /// during launch of the activity. 
        /// </summary>
        [XmlAttribute(SCORM.ReadMinScore)]
        public bool ReadMinScore; // = true

        /// <summary>
        /// This attribute indicates that the 
        /// maximum value in the range of the raw score for the identified local objective 
        /// should be retrieved (true or false) from the identified shared global objective 
        /// during launch of the activity. 
        /// </summary>
        [XmlAttribute(SCORM.ReadMaxScore)]
        public bool ReadMaxScore; // = true

        /// <summary>
        /// This attribute indicates 
        /// that the completion status for the identified local objective should be retrieved 
        /// (true or false) from the identified shared global objective when the completion 
        /// status for the global objective is defined [5].  
        /// </summary>
        [XmlAttribute(SCORM.ReadCompletionStatus)]
        public bool ReadCompletionStatus; // = true

        /// <summary>
        /// This attribute indicates 
        /// that the progress measure for the identified local objective should be retrieved 
        /// (true or false) from the identified shared global objective when the progress for 
        /// the global objective is defined [5].
        /// </summary>
        [XmlAttribute(SCORM.ReadProgressMeasure)]
        public bool ReadProgressMeasure; // = true

        /// <summary>
        /// This attribute indicates that 
        /// the raw score for the identified local objective should be transferred (true or 
        /// false) to the identified shared global objective upon termination ( 
        /// Termination(“”) ) of the attempt on the activity. 
        /// </summary>
        [XmlAttribute(SCORM.WriteRawScore)]
        public bool WriteRawScore; // = false

        /// <summary>
        /// This attribute indicates that 
        /// the minimum value in the range of the raw score for the identified local objective 
        /// should be transferred (true or false) to the identified shared global objective 
        /// upon termination ( Termination(“”) ) of the attempt on the activity. 
        /// </summary>
        [XmlAttribute(SCORM.WriteMinScore)]
        public bool WriteMinScore; // = false

        /// <summary>
        ///  This attribute indicates that 
        /// the maximum value in the range of the raw score for the identified local objective 
        /// should be transferred (true or false) to the identified shared global objective 
        /// upon termination ( Termination(“”) ) of the attempt on the activity. 
        /// </summary>
        [XmlAttribute(SCORM.WriteMaxScore)]
        public bool WriteMaxScore; // = false

        /// <summary>
        /// This attribute 
        /// indicates that the completion status for the identified local objective should be 
        /// transferred (true or false) to the identified shared global objective upon 
        /// termination ( Termination(“”) ) of the attempt on the activity. 
        /// </summary>
        [XmlAttribute(SCORM.WriteCompletionStatus)]
        public bool WriteCompletionStatus; // = false
        
        /// <summary>
        ///  This attribute 
        /// indicates that the progress measure for the identified local objective should be 
        /// transferred (true or false) to the identified shared global objective upon 
        /// termination ( Termination(“”) ) of the attempt on the activity.
        /// </summary>
        [XmlAttribute(SCORM.WriteProgressMeasure)]
        public bool WriteProgressMeasure; // = false
        
        #endregion
    }
}