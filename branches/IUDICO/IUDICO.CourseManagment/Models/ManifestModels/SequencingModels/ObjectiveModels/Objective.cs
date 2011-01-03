using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels
{
    [Serializable]
    public class Objective
    {
        #region XmlAttributes

        [XmlAttribute(SCORM.SatisfiedByMeasure)]
        public bool SatisfiedByMeasure; // =false;
        
        [XmlAttribute(SCORM.ObjectiveId)]
        public string ObjectiveId;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.MinNormalizedMeasure, Namespace = SCORM.ImsssNamespace)]
        public float MinNormalizedMeasure;

        [XmlElement(SCORM.MapInfo, Namespace = SCORM.ImsssNamespace)]
        public List<MapInfo> MapInfos;

        #endregion
    }
}