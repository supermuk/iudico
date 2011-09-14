using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [Serializable]
    public class RollupAction
    {
        #region XmlAttributes

        /// <summary>
        ///  This attribute indicates the desired rollup behavior if the 
        /// rule evaluates to true. 
        /// </summary>
        [XmlAttribute(SCORM.Action)]
        public RollupActions Action;

        #endregion
    }
}