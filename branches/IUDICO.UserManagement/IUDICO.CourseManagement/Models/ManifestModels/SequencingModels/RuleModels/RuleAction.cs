using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels
{
    [Serializable]
    public class RuleAction
    {
        #region XmlAttributes

        /// <summary>
        /// The action represents the desired sequencing behavior if the rule condition evaluates to true.
        /// </summary>
        [XmlAttribute(SCORM.Action)]
        public Action Action;

        #endregion
    }
}