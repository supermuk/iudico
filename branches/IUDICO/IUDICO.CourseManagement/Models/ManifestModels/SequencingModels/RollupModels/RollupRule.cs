using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public class RollupRule
    {
        #region XmlAttributes

        /// <summary>
        ///  This attribute indicates 
        /// whose data values are used to evaluate the rollup condition 
        /// </summary>
        [XmlAttribute(SCORM.ChildActivitySet)]
        public ChildActivitySet ChildActivitySet; // = ChildActivitySet.All;
        
        /// <summary>
        ///  The minimumCount attribute 
        /// shall be used when the childActivitySet attribute is set to atLeastCount.  
        /// The rollup rule condition evaluates to true if at least the number of children 
        /// specified by this attribute have a rollup condition of true.
        /// </summary>
        [XmlAttribute(SCORM.MinimumCount)]
        public int MinimumCount; // = 0;

        /// <summary>
        ///  The minimumPercent 
        /// attribute shall be used when the childActivitySet attribute is set to 
        /// atLeastPercent.  The rollup rule condition evaluates to true if at least the 
        /// percentage of children specified by this attribute have a rollup condition value 
        /// of true.
        /// </summary>
        [XmlAttribute(SCORM.MinimumPercent)]
        public double MinimumPercent; // = 0.0000;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.RollupConditions, Namespace = SCORM.ImsssNamespace)]
        public RollupConditions RollupConditions;

        [XmlElement(SCORM.RollupAction, Namespace = SCORM.ImsssNamespace)]
        public RollupAction RollupAction;

        #endregion
    }
}