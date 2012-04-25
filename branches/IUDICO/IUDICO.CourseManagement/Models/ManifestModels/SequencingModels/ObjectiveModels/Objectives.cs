using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels
{
    [Serializable]
    public class Objectives
    {
        #region XmlElements

        [XmlElement(SCORM.PrimaryObjective, Namespace = SCORM.ImsssNamespace)]
        public Objective PrivaryObjective;

        [XmlElement(SCORM.Objective)]
        public List<Objective> ObjectivesList;

        #endregion
    }
}