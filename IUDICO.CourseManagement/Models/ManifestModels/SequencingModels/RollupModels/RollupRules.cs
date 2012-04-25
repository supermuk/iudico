using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using IUDICO.Common.Models;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public partial class RollupRules
    {
        public RollupRules()
        {
            this.RollupRulesList = new List<RollupRule>();
        }

        #region XmlAttributes

        /// <summary>
        /// This attribute 
        /// indicates that the objective’s satisfied status associated with the activity is 
        /// included in the rollup for its parent activity.
        /// </summary>
        [XmlAttribute(SCORM.RollupObjectiveSatisfied)]
        public bool RollupObjectiveSatisfied { get; set; } // = true;
        /// <summary>
        /// This attribute 
        /// indicates that the attempt’s completion status associated with the activity is 
        /// included in the rollup for its parent activity.
        /// </summary>
        [XmlAttribute(SCORM.RollupProgressCompletion)]
        public bool RollupProgressCompletion { get; set; } // = true;

        /// <summary>
        /// This attribute 
        /// indicates the weighting factor applied to the objectives normalized measure used 
        /// during rollup for the parent activity.
        /// </summary>
        [XmlAttribute(SCORM.ObjectiveMeasureWeight)]
        public float ObjectiveMeasureWeight { get; set; }

        #endregion

        #region XmlElements

        [XmlElement(SCORM.RollupRule)]
        public List<RollupRule> RollupRulesList { get; set; }

        #endregion
    }
}