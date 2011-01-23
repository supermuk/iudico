using System.Collections.Generic;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    public class SequencingCollection
    {
        #region XmlElements

        [XmlElement(SCORM.Sequencing, Namespace = SCORM.ImsssNamespace)]
        public List<Sequencing> Sequencings;

        #endregion
    }
}