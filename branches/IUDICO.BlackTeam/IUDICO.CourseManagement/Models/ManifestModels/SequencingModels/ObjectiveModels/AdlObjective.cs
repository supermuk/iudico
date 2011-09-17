using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels
{
    [Serializable]
    public class AdlObjective
    {
        #region XmlAttributes

        [XmlAttribute(SCORM.ObjectiveId, Namespace = SCORM.AdlseqNamespace)]
        public string ObjectiveId;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.MapInfo, Namespace = SCORM.AdlseqNamespace)]
        public List<MapInfo> MapInfos;

        #endregion
    }
}