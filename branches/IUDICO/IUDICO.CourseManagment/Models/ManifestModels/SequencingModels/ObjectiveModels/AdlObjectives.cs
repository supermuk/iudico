using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels
{
    public class AdlObjectives
    {
        #region XmlElements

        [XmlElement(SCORM.Objective, Namespace = SCORM.AdlseqNamespace)]
        public List<AdlObjective> Objectives;

        #endregion
    }
}